using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Courier;

namespace Enterprise_E_Commerce_Management_System.Application.Couriers.Mapping
{
    public class CourierProfile:Profile
    {
        public CourierProfile()
        {
            CreateMap<CourierItemDTO, CourierItemViewModel>()
                .ForMember(vm=>vm.Status,options=>options.MapFrom(dto=>dto.Status?"Active":"Inactive"));
            CreateMap<CourierFilterDTO,CourierFilterViewModel>().ReverseMap();
            CreateMap<CourierPagedListDTO, CourierPagedListViewModel>();
            CreateMap<CourierDetailsDTO, CourierDetailsViewModel>();
        }
    }
}
