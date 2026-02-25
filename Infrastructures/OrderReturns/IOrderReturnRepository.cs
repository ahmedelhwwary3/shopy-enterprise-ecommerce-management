using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.OrderReturns
{
    public interface IOrderReturnRepository : IRepository<OrderReturn>
    {
       Task<OrderReturn> GetByOrderIdAsync(int  orderId);
    }
}
