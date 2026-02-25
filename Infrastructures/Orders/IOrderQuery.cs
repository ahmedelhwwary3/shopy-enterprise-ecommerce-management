using Enterprise_E_Commerce_Management_System.Application.Orders;
using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Orders
{
    /// <summary>
    /// Provides explicit, read-only queries designed for specific
    /// business and presentation scenarios. Queries return DTOs
    /// and are optimized for complex or performance-sensitive reads.
    /// </summary>
    public interface IOrderQuery
    {
       Task<int>CreateAndGetIdAsync(Order order);
       Task<OrderManagementPagedListDTO> GetUserAssignedListAsync(OrderManagementFilterDTO filter,int currencyId); 
    }
}
