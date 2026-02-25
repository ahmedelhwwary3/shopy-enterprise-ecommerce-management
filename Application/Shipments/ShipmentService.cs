
using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.OrderReturns;
using Enterprise_E_Commerce_Management_System.Application.Orders;
using Enterprise_E_Commerce_Management_System.Application.Payments;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.Results;
using Enterprise_E_Commerce_Management_System.Application.Variants;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Shipments;
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using System.Collections.Generic;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments
{
    public class ShipmentService : IShipmentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IShipementQuery _query;
        private readonly IPaymentService _paymentService;
        private readonly IShipmentOrderService _shipmentOrderService;
        private readonly IOrderReturnService _orderReturnService;
        public ShipmentService
            (IUnitOfWork uow,
            IMapper mapper,
            IShipementQuery query,
            IPaymentService paymentService,
            IShipmentOrderService shipmentOrderService,
            IOrderReturnService orderReturnService)
        {
            _uow = uow;
            _query = query; 
            _mapper = mapper;
            _paymentService=paymentService;
            _shipmentOrderService=shipmentOrderService;
            _orderReturnService=orderReturnService;
        }
        public async Task AssignForCourierAsync(int orderId, int courierId)
        {
            var shipment = new Shipment()
            {
                AssignedDate = DateTime.UtcNow,
                CourierId = courierId,
                OrderId = orderId,
                ShipmentStatus = enShippingStatus.AssignedForCourier,
                OrderStatus=enOrderStatus.InDelivery
            };
            await _uow.Shipments.AddAsync(shipment); 
        }

        public List<ShipmentStatusItemViewModel> GetShipmentStatusItemViewModel()
        {
            return Enum.GetValues(typeof(enShippingStatus))
                .Cast<enShippingStatus>()
                .Select(value => new ShipmentStatusItemViewModel()
                {
                    Name = value.ToString(),
                    Value = value
                }).ToList();
        }

        public async Task<AssignedShipmentPagedListViewModel>
            GetCourierAssignedOrdersViewModelAsync(AssignedShipmentFilterViewModel filter,int currencyId)
        {
            var statusList = GetShipmentStatusItemViewModel();
            var filterDTO=_mapper.Map<AssignedShipmentFilterDTO>(filter);
            var listDTO = await _query.GetCourierAssignedOrderListViewModelAsync(filterDTO, currencyId);
            var listVM = _mapper.Map<AssignedShipmentPagedListViewModel>(listDTO); 
            return listVM;
        }

        public async Task<AvailableOrderPagedListViewModel>
          GetAvailableOrdersForCourierAsync(AvailableOrderFilterViewModel filter, int currencyId)
        {
            var filterDTO = _mapper.Map<AvailableOrderFilterDTO>(filter);
            var listDTO = await _query.GetAvailableOrdersForCourierAsync(filterDTO, currencyId);
            var listVM = _mapper.Map<AvailableOrderPagedListViewModel>(listDTO);
            return listVM;
        }

        public async Task<ShipmentDetailsViewModel> GetDetailsViewModelByOrderIdAsync(int OrderId, int currencyId)
        {
            var dto = await _uow.Shipments.GetDetailsDtoByOrderIdAsync(OrderId,currencyId);
            var viewModel = _mapper.Map<ShipmentDetailsViewModel>(dto);
            return viewModel;
        }

        public async Task<Shipment> GetLastByOrderIdAsync(int orderId)
        {
            return await _uow.Shipments.GetLastByOrderIdAsync(orderId);
        }

        public async Task<ConfirmShipmentViewModel> GetConfirmViewModelByIdAsync(int shipmentId)
        {
            var dto = await _uow.Shipments.GetConfirmDtoByIdAsync(shipmentId);
            var viewModel = _mapper.Map<ConfirmShipmentViewModel>(dto);
            viewModel.ShipmentStatusList = GetShipmentStatusListViewModel(viewModel.LastStatus);
            return viewModel;
        }
        public List<ShipmentStatusItemViewModel> GetShipmentStatusListViewModel
            (enShippingStatus shipmentStatus)
        { 
            var list = new List<ShipmentStatusItemViewModel>();
            if (shipmentStatus == enShippingStatus.AssignedForCourier)
            {
                list.AddRange(new List<ShipmentStatusItemViewModel>()
                        {
                            new ()
                        {
                            Name = "Shipped",
                            Value = enShippingStatus.Shipped
                        }  ,new ()
                        {   Name = "Shipping Failed",
                            Value = enShippingStatus.ShippingFailed
                        }
                        });
                return list;
            } 
            throw new Exception("Invalid Shipment Status.");
        }

        // 4
        public async Task<enConfirmShipmentResult> ConfirmShipmentAsync(int shipmentId,enShippingStatus ShipmentStatus)
        {
            if (ShipmentStatus == enShippingStatus.AssignedForCourier)
                return enConfirmShipmentResult.InvalidShipmentStatus; 

            //Get Entities For Update (shipment,Order)
            Shipment shipment =await _uow.Shipments.GetByIdAsync(shipmentId);
            bool IsToReturn = await _paymentService.ExistsByOrderIdAsync(shipment.OrderId); 
            if (shipment == null)
                return enConfirmShipmentResult.ShipmentNotFound; 
             
            Order order = await _uow.Orders.GetByShipmentIdAsync(shipmentId);
            if (order == null)
                return enConfirmShipmentResult.OrderNotFound;

            //Handle All cases
            shipment.ShipmentStatus = ShipmentStatus; 
            bool IsDeliveryFail = ShipmentStatus == enShippingStatus.ShippingFailed;
            bool IsDeliverySuccess = ShipmentStatus == enShippingStatus.Shipped;
            //increate => 1.normal fail 2.return success
            bool IsIncreaseOnFail = IsDeliveryFail && !IsToReturn;
            bool IsIncreaseOnDelivered = IsDeliverySuccess && IsToReturn;
            bool IsIncreaseQuantity = IsIncreaseOnFail || IsIncreaseOnDelivered;
            //Update StockQuantity
            if (IsIncreaseQuantity)//1.normal fail 2.return success
            { 
                shipment.AssignedDate = DateTime.UtcNow;
                await _shipmentOrderService
                         .RecalculateStockQuantityAndStatusAsync(IsRemove: false, order.Id);
            }
            //Update Delivered Data (Order,Shipment,Payment)
            if (IsDeliverySuccess)
            {

                shipment.DeliveredDate = DateTime.UtcNow;
                order.OrderStatus = enOrderStatus.Paid;
                shipment.OrderStatus = enOrderStatus.Paid;//SnapShot
                if (!IsToReturn)//Frist Time
                {
                    //Create Payment If Shipment confirm succeeded
                    Payment payment = new Payment()
                    {
                        Amount = order.TotalAmount,
                        CustomerId = order.CustomerId,
                        OrderId = order.Id,
                        PaymentDate = DateTime.UtcNow,
                        PaymentMethod = enPaymentMethod.CashOnDelivery,
                        PaymentStatus = enPaymentStatus.Paid
                    };
                    await _paymentService.AddAsync(payment);
                }
            } 
            if(IsDeliveryFail)
            {
                order.OrderStatus = enOrderStatus.Cancelled;
                shipment.OrderStatus = enOrderStatus.Cancelled;//SnapShot
            }
            //Update orderReturn 
            if (IsToReturn)
            {
                var orderReturn = await _orderReturnService.GetByOrderIdAsync(order.Id);
                if (orderReturn == null)
                    return enConfirmShipmentResult.OrderReturnNotFound;
                if (IsDeliverySuccess)
                {
                    orderReturn.ReturnedAt = DateTime.UtcNow;
                    orderReturn.Status = enOrderReturnStatus.Returned;
                }
                else
                    orderReturn.Status = enOrderReturnStatus.Rejected;
            }
            
            await _uow.SaveChangesAsync();
            return enConfirmShipmentResult.Success;
        } 
    }
}
