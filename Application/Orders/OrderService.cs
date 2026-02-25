using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Carts;
using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Customers;
using Enterprise_E_Commerce_Management_System.Application.Emails;
using Enterprise_E_Commerce_Management_System.Application.Emails.Results;
using Enterprise_E_Commerce_Management_System.Application.OrderItems;
using Enterprise_E_Commerce_Management_System.Application.OrderItems.Results;
using Enterprise_E_Commerce_Management_System.Application.OrderReturns;
using Enterprise_E_Commerce_Management_System.Application.Orders.Results;
using Enterprise_E_Commerce_Management_System.Application.Payments;
using Enterprise_E_Commerce_Management_System.Application.Products;
using Enterprise_E_Commerce_Management_System.Application.Shipments;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.Results;
using Enterprise_E_Commerce_Management_System.Application.Variants;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Orders;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.OrderItems;
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;
using Enterprise_E_Commerce_Management_System.ViewModels.Orders;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;
        private readonly IUnitOfWork _uow;
        private readonly IOrderItemService _orderItemService;
        private readonly IShipmentOrderService _shipmentOrderService; 
        private readonly IMapper _mapper; 
        private readonly IShipmentService _shipmentService;
        private readonly IOrderQuery _query;
        private readonly ICustomerService _customerService;
        private readonly IEmailSerivce _emailSerivce;
        private readonly IOrderReturnService _orderReturnService;
        private readonly IPaymentService _paymentService;
        public OrderService(
            ICartService cartService,
            IUnitOfWork uow,
            IOrderItemService orderItemService,
            IProductService productService,
            IVariantService variantService,
            IMapper mapper, 
            IShipmentService shipmentService,
            IOrderQuery query,
            IShipmentOrderService shipmentOrderService,
            ICustomerService customerService,
            IEmailSerivce emailSerivce,
            IOrderReturnService orderReturnService,
            IPaymentService paymentService)
        {
            _cartService = cartService;
            _orderItemService = orderItemService; 
            _uow = uow;
            _mapper = mapper; 
            _shipmentService = shipmentService;
            _customerService= customerService;
            _shipmentOrderService= shipmentOrderService;
            _query = query;
            _emailSerivce= emailSerivce;
            _orderReturnService= orderReturnService;
            _paymentService= paymentService;
        }//1
        public async Task<enCreateOrderResult> CreateOrderAndSendTokenAsync(int customerId, int cartId)
        {
            var customer = await _customerService.GetAsReadOnlyByIdAsync(customerId);
            if (customer == null)
                return enCreateOrderResult.CustomerNotFound;
            var cart = await _cartService
                 .GetWithItemsByIdAsync(cartId);
            if (cart == null)
                return enCreateOrderResult.CartNotFound;
            //1.Create Order
            var order = new Order(); 
            //Customer SnapShot
            order.Address = customer.Address;//Country,PostalCode,City,Street 
            order.CustomerId = customerId;
            order.Email=customer.Email;
            order.PhoneNumber=customer.PhoneNumber;
            //order.OrderNumber = string.Concat("ORD-", order.Id);//DB Trigger
            order.AccessToken = Guid.NewGuid().ToString();
            order.OrderStatus = enOrderStatus.Pending;//Will be available for couriers
            order.OrderDate = DateTime.UtcNow; 
            decimal totalAmount = 0;
            foreach (var item in cart.CartItems)
            {
                totalAmount += item.Quantity * item.UnitPrice;
            }
            order.TotalAmount = totalAmount;
            await _uow.Orders.AddAsync(order);//Id Created

            //2.Send Email
            enSendEmailResult mailResult=_emailSerivce
                .SendOrderEmail(order.Email,order.AccessToken);
            if (mailResult==enSendEmailResult.InvalidData)
                return enCreateOrderResult.InvalidMailData;

            //3.Update Cart
            cart.CustomerId = customerId;
            _cartService.Update(cart);

            //4.Copy CartItems To OrderItems
            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem();
                orderItem.VariantId = item.VariantId;
                orderItem.Quantity = item.Quantity;
                orderItem.Price = item.UnitPrice;
                orderItem.Order = order;//Created (Nav Proberty)
                enAddOrderItemResult itemResult = await _orderItemService
                    .AddAsync(orderItem);
                if (itemResult==enAddOrderItemResult.InvalidData)
                    return enCreateOrderResult.InvalidItemData;
                //5.Delete CartItems
                await _uow.CartItems.DeleteByIdAsync(item.Id);
            }
            //6.Delete Cart
            await _uow.Carts.DeleteByIdAsync(cart.Id);
            await _uow.SaveChangesAsync();
            return enCreateOrderResult.Success;
        }
        // 3
         
        public async Task<enAssignOrderForShipmentResult> AssignForShipmentAsync(int orderId, int courierId)
        { 
            //1.check Rules & Validation
            var orderDB = await _uow.Orders.GetIncludeItemsByOrderIdAsync(orderId);
            if (orderDB == null)
                return enAssignOrderForShipmentResult.OrderNotFound;

            if (orderDB?.OrderItems == null)
                return enAssignOrderForShipmentResult.OrderItemsNotFound;

            if (orderDB == null || orderDB?.OrderItems == null ||
                (orderDB.OrderStatus != enOrderStatus.Paid &&
               orderDB.OrderStatus != enOrderStatus.Pending))
                return enAssignOrderForShipmentResult.InvalidOrderStatus;

            //2.Create ReadyForDelivery Shipment
            await _shipmentService.AssignForCourierAsync(orderId, courierId);
            //3.If Pending => recalculate StockQuantity To Avoid Race Condition "exceeding amount deliverey" 
            if (orderDB.OrderStatus == enOrderStatus.Pending)
            {
                var recalculateSuccess = await _shipmentOrderService
                    .RecalculateStockQuantityAndStatusAsync(IsRemove: true, orderId);
                if (recalculateSuccess == enRecalculateStockQuantityStatusResult.OrderVariantNotFound)
                    return enAssignOrderForShipmentResult.OrderVariantNotFound;
                if (recalculateSuccess == enRecalculateStockQuantityStatusResult.OrderItemsNotFound)
                    return enAssignOrderForShipmentResult.OrderItemsNotFound; 
            }
            else
            {
                //4.If Paid (Return):-
                //- recalculate StockQuantity will be after delivert done for ensurement in:
                //  ShipmentService.ConfirmShipmentAsync()
                //- Update OrderReturn 
                var orderReturn = await _orderReturnService.GetByOrderIdAsync(orderId);
                if (orderReturn == null)
                    return enAssignOrderForShipmentResult.PaidOrderReturnNotFound;
                orderReturn.PickedUpAt = DateTime.UtcNow;
                orderReturn.CourierId= courierId;
                orderReturn.Status = enOrderReturnStatus.PickedUp;
            }
            orderDB.OrderStatus = enOrderStatus.InDelivery; 
            await _uow.SaveChangesAsync();//Single Transaction
            return enAssignOrderForShipmentResult.Success;
        }

        public async Task<OrderManagementPagedListViewModel> 
            GetUserAssignedListAsync(OrderManagementFilterViewModel filter, int currencyId)
        {
            var filterDTO = _mapper.Map<OrderManagementFilterDTO>(filter); 
            var listDTO = await _query.GetUserAssignedListAsync(filterDTO, currencyId);
            var listVM = _mapper.Map<OrderManagementPagedListViewModel>(listDTO);

            return listVM;
        }

        public async Task<OrderDetailsViewModel> GetDetailsViewModelByIdAsync(int orderId)
        {
            var dto= await _uow.Orders.GetDetailsDtoByIdAsync(orderId);
            var viewModel = _mapper.Map<OrderDetailsViewModel>(dto);
            return viewModel;
        }

        public async Task<OrderTrackViewModel> GetTrackViewModelAsync(string token,int currencyId)
        {
            var dto = await _uow.Orders.GetTrackDtoByTokenAsync(token, currencyId);
            var viewModel = _mapper.Map<OrderTrackViewModel>(dto);
            return viewModel;
        }
        // 2
        public async Task<enCancelOrReturnOrderResult> CancelOrReturnAsync(int orderId,string notes) 
        {
            var orderDB=await _uow.Orders.GetByIdAsync(orderId,o=>o.Shipments);
            bool IsReturn = await _paymentService.ExistsByOrderIdAsync(orderId);
            if (IsReturn) 
            {
                //2.Paid will still Paid but will have OrderReturn.Status = Requested
                //check business rules + create return 
                DateTime deliveredDate = orderDB.Shipments.OrderBy(sh => sh.Id).Last().DeliveredDate.Value;
                if (deliveredDate.AddDays(valid.OrderReturnMaxDays) < DateTime.Now)
                    return enCancelOrReturnOrderResult.ExceedsReturnMaxDays;  
                var orderReturnDB = new OrderReturn()
                {
                    OrderId = orderId,
                    RequestedAt = DateTime.UtcNow,
                    Status = enOrderReturnStatus.Requested,//Now This order will be Available for Couriers
                    Notes = notes
                }; 
                await _orderReturnService.AddAsync(orderReturnDB);
            }
            else//Cancel
            {
                orderDB.OrderStatus = enOrderStatus.Cancelled;
                var shipmentDB=await _shipmentService.GetLastByOrderIdAsync(orderId);
                if(shipmentDB!=null)
                {
                    shipmentDB.OrderStatus=enOrderStatus.Cancelled;
                }
            } 
            await _uow.SaveChangesAsync();
            return enCancelOrReturnOrderResult.Success;
        }

        
    }

}
