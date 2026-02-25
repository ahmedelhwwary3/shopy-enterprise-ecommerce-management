using Enterprise_E_Commerce_Management_System.Application.Shipments.Results;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using Microsoft.AspNetCore.Identity;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments
{
    public interface IShipmentService
    {
        Task AssignForCourierAsync(int orderId,int courierId);
        Task<AssignedShipmentPagedListViewModel> GetCourierAssignedOrdersViewModelAsync
            (AssignedShipmentFilterViewModel courierId,int currencyId);
        Task<AvailableOrderPagedListViewModel> GetAvailableOrdersForCourierAsync
            (AvailableOrderFilterViewModel filter,int currencyId);
        Task<ShipmentDetailsViewModel> GetDetailsViewModelByOrderIdAsync(int OrderId,int currencyId);
        Task<Shipment> GetLastByOrderIdAsync(int orderId);
        Task<ConfirmShipmentViewModel> GetConfirmViewModelByIdAsync(int shipmentId);
        Task<enConfirmShipmentResult> ConfirmShipmentAsync(int shipmentId,enShippingStatus status);
        List<ShipmentStatusItemViewModel> GetShipmentStatusListViewModel(enShippingStatus shipmentStatus);
    }
}
