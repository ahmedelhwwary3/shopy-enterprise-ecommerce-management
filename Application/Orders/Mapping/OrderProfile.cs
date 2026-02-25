using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;
using Enterprise_E_Commerce_Management_System.ViewModels.Orders;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;

namespace Enterprise_E_Commerce_Management_System.Application.Orders.Mapping
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<CustomerDetailsWithCountryListViewModel, CreateOrderViewModel>();
            CreateMap<OrderManagementFilterDTO, OrderManagementFilterViewModel>().ReverseMap();
            CreateMap<OrderManagementItemDTO, OrderManagementItemViewModel>();
            CreateMap<OrderManagementPagedListDTO, OrderManagementPagedListViewModel>();
            CreateMap<OrderDetailsDTO, OrderDetailsViewModel>();
            CreateMap<OrderTrackDTO, OrderTrackViewModel>()
                .ForMember(vm=>vm.IsCancel,options=>options
                .MapFrom(dto=>dto.OrderStatus==enOrderStatus.InDelivery||
                dto.OrderStatus == enOrderStatus.Pending));
        }
    }
}
