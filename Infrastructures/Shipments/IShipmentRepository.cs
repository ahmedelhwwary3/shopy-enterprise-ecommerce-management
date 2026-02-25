using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Shipments
{
    public interface IShipmentRepository : IRepository<Shipment>
    {
        Task<ShipmentDetailsDTO> GetDetailsDtoByOrderIdAsync(int orderId,int currencyId);
        Task<Shipment> GetLastByOrderIdAsync(int orderId);
        Task<ConfirmShipmentDTO> GetConfirmDtoByIdAsync(int shipmentId);
    }
}
