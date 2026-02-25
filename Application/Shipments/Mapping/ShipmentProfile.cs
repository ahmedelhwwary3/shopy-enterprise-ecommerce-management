using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments.Mapping
{
    public class ShipmentProfile:Profile
    {
        public ShipmentProfile()
        {
            CreateMap<AssignedShipmentFilterViewModel,AssignedShipmentFilterDTO>().ReverseMap();
            CreateMap<AssignedShipmentItemDTO, AssignedShipmentListItemViewModel>();
            CreateMap<AssignedShipmentPagedListDTO, AssignedShipmentPagedListViewModel>(); 
            CreateMap<AvailableOrderFilterViewModel, AvailableOrderFilterDTO>().ReverseMap();
            CreateMap<AvailableOrdersPagedListDTO, AvailableOrderPagedListViewModel>();
            CreateMap<AvailableOrderItemDTO, AvailableOrderItemViewModel>()
                .ForMember(vm=>vm.Status,options=>options.MapFrom(dto=>dto.IsReturn?"Return":"New"));
            CreateMap<ShipmentDetailsDTO, ShipmentDetailsViewModel>()
                .ForMember(vm => vm.Status, options => options.MapFrom(dto => dto.Status.ToString()));
            CreateMap<ConfirmShipmentDTO, ConfirmShipmentViewModel>()
                .ForMember(vm=>vm.ConfirmStatus,options=>options.Ignore());

        }
    }
}
