using Enterprise_E_Commerce_Management_System.Application.OrderReturns.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Orders;
using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.OrderReturns
{
    public interface IOrderReturnQuery
    {
        Task<OrderReturnPagedListDTO> GetAllListAsync(OrderReturnFilterDTO filter,int currencyId);
    }
}
