using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Couriers;
using Enterprise_E_Commerce_Management_System.Application.Customers;
using Enterprise_E_Commerce_Management_System.Application.Customers.Results;
using Enterprise_E_Commerce_Management_System.Application.Emails;
using Enterprise_E_Commerce_Management_System.Application.Orders;
using Enterprise_E_Commerce_Management_System.Application.Orders.Results;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService; 
        private readonly ICustomerService _customerService;
        private readonly ICourierService _courierService;
        public OrderController(
            IOrderService orderService
            , ICustomerService customerService,
            ICourierService courierService)
        {
            _customerService = customerService;
            _orderService = orderService;  
            _courierService= courierService;
        }

        [HttpGet]
        [Authorize(Policy = "Orders.UsersViewAssigned")] 
        public async Task<IActionResult> ManageAssignedOrders(OrderManagementPagedListViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (viewModel.Filter == null)
                viewModel.Filter = new OrderManagementFilterViewModel();

            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            viewModel = await _orderService.GetUserAssignedListAsync(viewModel.Filter, currencyId); 
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }
         
        [HttpGet]
        [Authorize(Policy = "Orders.UsersViewAssigned")] 
        public async Task<IActionResult> Details(int OrderId)
        {
             
            if (OrderId <= 0)
                return BadRequest();

            var viewModel = await _orderService.GetDetailsViewModelByIdAsync(OrderId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        } 

        [HttpPost]
        [Authorize(Policy = "Orders.AcceptShipment")]
        public async Task<IActionResult> AssignOrderForShipment(int OrderId) 
        {
            if (OrderId <= 0)
                return BadRequest();

            int courierId = int.Parse(User.FindFirstValue(storage.CourierIdKey));//Courier is User
            if(courierId<=0)
                return BadRequest();

            enAssignOrderForShipmentResult result = await _orderService
                .AssignForShipmentAsync(OrderId,courierId);
            if(result==enAssignOrderForShipmentResult.InvalidOrderStatus)
                return BadRequest("Invalid order status.");

            if (result == enAssignOrderForShipmentResult.PaidOrderReturnNotFound)
                return NotFound("Paid order return not found.");

            if (result == enAssignOrderForShipmentResult.OrderItemsNotFound)
                return NotFound("Order items not found.");

            if (result == enAssignOrderForShipmentResult.OrderVariantNotFound)
                return NotFound("Order variant not found.");

            if (result == enAssignOrderForShipmentResult.OrderNotFound)
                return NotFound("Order not found.");
            //Success
            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            return RedirectToAction("CourierAssignedOrders", "Shipment");
        }  


        //Customer (Guest) Actions:-
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CheckOut()
        {
            //1.Check If There is a CartId (Session||Cookies) To CheckOut
            if(!storage.GetInt32FromSessionOrCookies
                (storage.CartIdKey,HttpContext).HasValue)
                return NotFound();

            //2.Check If There is a CustomerId (Cookies) To Show Details instead of entering inputs
            var customerId = storage.GetInt32FromCookies("CustomerId", HttpContext);
            //Show Details instead of entering inputs
            if (customerId.HasValue)
            {
                var viewModel = await _customerService
                .GetFormViewModelAsync(customerId.Value);
                if (viewModel == null)
                    viewModel = await _customerService.GetFormViewModelAsync();

                return View(viewModel);
            }
            else
            {
                //Enter Customer Data Manually (No Customer in Cookies)
                var viewModel = await _customerService
                    .GetFormViewModelAsync();
                if (viewModel == null)
                    return NotFound();

                return View(viewModel);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm(CustomerFormViewModel viewModel)
        {
            int? cartId = storage
                .GetInt32FromSessionOrCookies(storage.CartIdKey, HttpContext);
            if (!cartId.HasValue)
                return NotFound("Cart is not found.");

            int? customerId = storage.GetInt32FromCookies("CustomerId",HttpContext);
            //If Guest is Customer , You are in UPDATE Mode
            //If not ,You are in FIND/CREATE mode. system will =>
            //1.Find the same customer by mail||phone or 2.create new (No Update)
            if(customerId.HasValue)
            {
                //Update Mode
                viewModel.Id=customerId.Value;
                enUpdateCustomerResult result = await _customerService.UpdateAsync(viewModel);
                if (result==enUpdateCustomerResult.CustomerNotFound)
                    return NotFound("Customer not found.");
            }
            else
            {
                //Identifying Mode
                enCreateCustomerResult result=await _customerService.CreateIfNotExistsAsync(viewModel);
                if (result==enCreateCustomerResult.CustomerExists)
                {
                    customerId = await _customerService
                        .GetIdByEmailOrPhoneAsync(viewModel.Email, viewModel.PhoneNumber); 
                }
            }
            storage.RefreshCookieDays(storage.CustomerIdKey,customerId.Value,HttpContext
                ,valid.CustomerCookieMonths*30);
            //take (custId) and attach with order/cart
            //find cart items , copy values to (create) order items .
            //Update Variant/Product Status&Quantity .
            enCreateOrderResult orderResult = await _orderService
                .CreateOrderAndSendTokenAsync(customerId.Value, cartId.Value);
            if (orderResult==enCreateOrderResult.CartNotFound)
                return NotFound("Cart not found.");

            if (orderResult == enCreateOrderResult.CustomerNotFound)
                return NotFound("Customer not found.");

            if (orderResult == enCreateOrderResult.InvalidItemData)
                return NotFound("Invalid item data.");

            if (orderResult == enCreateOrderResult.InvalidMailData)
                return BadRequest("Invalid mail data."); 

            storage.RemoveSessionAndCookie(storage.CartIdKey,HttpContext);
            this.SendTempMessage(enTempMessage.CreateSucceeded);
            return RedirectToAction("Index", "Shopping");
        }

        [HttpGet]//  /Order/Track?AccessToken=
        [AllowAnonymous]
        public async Task<IActionResult> Track(string AccessToken)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return BadRequest();
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            var viewModel = await _orderService.GetTrackViewModelAsync(AccessToken, currencyId);
            if (viewModel == null)
                return Unauthorized();

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CancelOrReturn(CancelOrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            enCancelOrReturnOrderResult result = await _orderService
                .CancelOrReturnAsync(viewModel.OrderId, viewModel.Notes);

            if (result==enCancelOrReturnOrderResult.ExceedsReturnMaxDays)
                return BadRequest("Exceeds returnMax days.");

            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            return RedirectToAction("Index","Shopping");
        }

        [HttpGet]
        [Authorize(Policy = "Shipments.ViewAvailableOrders")]
        public async Task<IActionResult> Count()
        {
            int? count = null;
            if (int.TryParse(User.FindFirstValue(storage.CourierIdKey),out int Id))
            {
                count = await _courierService.GetAssignedOrdersCountByCourierIdAsync(Id);
                storage.RefreshSession(storage.OrdersCountKey,count.Value,HttpContext);
                storage.RefreshCookie(storage.OrdersCountKey, count.Value, HttpContext);
            }  
            return Json(count);
        }
    }
}
