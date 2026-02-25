using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.OrderItems.Results;
using Enterprise_E_Commerce_Management_System.Models.OrderItems;

namespace Enterprise_E_Commerce_Management_System.Application.OrderItems
{
    public interface IOrderItemService
    {
        Task<enAddOrderItemResult> AddAsync(OrderItem item); 

    }
}
