using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.OrderReturns.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.OrderReturn;

namespace Enterprise_E_Commerce_Management_System.Application.OrderReturns.Mapping
{
    public class OrderReturnProfile:Profile
    {
        public OrderReturnProfile()
        {
            CreateMap<OrderReturnItemDTO,OrderReturnItemViewModel>();
            CreateMap<OrderReturnPagedListDTO,OrderReturnPagedListViewModel>();
            CreateMap<OrderReturnFilterDTO, OrderReturnFilterViewModel>().ReverseMap();
        }
    }
}
