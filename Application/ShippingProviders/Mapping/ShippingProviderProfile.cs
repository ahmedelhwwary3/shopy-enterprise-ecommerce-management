using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider;

namespace Enterprise_E_Commerce_Management_System.Application.ShippingProviders.Mapping
{
    public class ShippingProviderProfile : Profile
    {
        public ShippingProviderProfile()
        {
            CreateMap<ShippingProviderNameIdDTO,ShippingProviderIdNameViewModel>();
            CreateMap<ShippingProviderFilterViewModel, ShippingProviderFilterDTO>().ReverseMap();
            CreateMap<ShippingProviderItemDTO, ShippingProviderItemWithCouriersCountViewModel>();
            CreateMap<ShippingProviderPagedListDTO, ShippingProviderPagedListViewModel>();
        }
    }
}
