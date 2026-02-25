using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Orders
{
    public interface IOrderRepository:IRepository<Order>
    {
        
        Task<Order> GetIncludeItemsByOrderIdAsync(int orderId);
        Task<OrderDetailsDTO> GetDetailsDtoByIdAsync(int orderId); 
        Task<Order> GetByShipmentIdAsync(int shipmentId);
        Task<OrderTrackDTO> GetTrackDtoByTokenAsync(string token,int currencyId);
    }
}
