using Enterprise_E_Commerce_Management_System.Application.Couriers;
using Enterprise_E_Commerce_Management_System.Application.Orders;
using Enterprise_E_Commerce_Management_System.Application.Shipments;
using Enterprise_E_Commerce_Management_System.Application.Shipments.Results;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    
    public class ShipmentController : Controller
    {
        private readonly IShipmentService _shipmentService;
        private readonly ICourierService _courierService;
        public ShipmentController(
            IShipmentService shipmentService,
            ICourierService courierService)
        {
            _shipmentService = shipmentService; 
            _courierService= courierService;
        } 


        [HttpGet]
        [Authorize(Policy = "Shipments.ManageCourierOrders")]
        public async Task<IActionResult> CourierAssignedOrders(AssignedShipmentPagedListViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return NotFound(); 
           
            int courierId = int.Parse(User.FindFirstValue(storage.CourierIdKey));//Courier is User 
            if (viewModel.Filter == null)
                viewModel.Filter = new AssignedShipmentFilterViewModel();
             
            viewModel.Filter.CourierId = courierId;
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            viewModel = await _shipmentService
                .GetCourierAssignedOrdersViewModelAsync(viewModel.Filter, currencyId);
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "Shipments.ManageCourierOrders")]
        public async Task<IActionResult> Confirm(int shipmentId)

        {
            var viewModel = await _shipmentService.GetConfirmViewModelByIdAsync(shipmentId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Shipments.ManageCourierOrders")]
        public async Task<IActionResult> Confirm(int shipmentId,enShippingStatus ConfirmStatus)
        {
            if (ConfirmStatus <= 0 || shipmentId <= 0)
                return BadRequest();

            enConfirmShipmentResult result = await _shipmentService.ConfirmShipmentAsync(shipmentId, ConfirmStatus);
            switch(result)
            {
                case enConfirmShipmentResult.InvalidShipmentStatus: 
                    {
                        return BadRequest("Invalid shipment Status.");
                    }
                case enConfirmShipmentResult.ShipmentNotFound:
                    {
                        return NotFound("Shipment not found.");
                    }
                case enConfirmShipmentResult.OrderNotFound:
                    {
                        return NotFound("Order not found.");
                    }
                case enConfirmShipmentResult.OrderReturnNotFound:
                    {
                        return NotFound("Order return not found.");
                    }
                case enConfirmShipmentResult.Success:
                    {
                        break;
                    }
            } 
            //Refresh OrderCount
            int? count = null;
            if (int.TryParse(User.FindFirstValue(storage.CourierIdKey), out int Id))
            {
                count = await _courierService.GetAssignedOrdersCountByCourierIdAsync(Id);
                storage.RefreshSession(storage.OrdersCountKey, count.Value, HttpContext);
                storage.RefreshCookie(storage.OrdersCountKey, count.Value, HttpContext);
            }

            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            return RedirectToAction(nameof(CourierAssignedOrders));
        } 

        [HttpGet]
        [Authorize(Policy = "Shipments.ViewAvailableOrders")]
        public async Task<IActionResult> AvailableOrdersForAssign(AvailableOrderPagedListViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (viewModel.Filter == null)
                viewModel.Filter = new AvailableOrderFilterViewModel();

            if (int.TryParse(User.FindFirstValue(storage.CountryIdKey), out int Id))
                viewModel.Filter.CountryId = Id;

            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
                viewModel = await _shipmentService
                .GetAvailableOrdersForCourierAsync(viewModel.Filter, currencyId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }//count

        [HttpGet]
        [Authorize(Policy = "Shipments.ManageCourierOrders")]
        public async Task<IActionResult> Details(int OrderId)
        {
            if (OrderId <= 0)
                return BadRequest();
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            var viewModel = await _shipmentService.GetDetailsViewModelByOrderIdAsync(OrderId, currencyId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }
    }
}
