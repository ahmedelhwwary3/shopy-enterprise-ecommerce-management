
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Enterprise_E_Commerce_Management_System.ViewModels.OrderReturn; 
using Microsoft.AspNetCore.Mvc;

namespace Enterprise_E_Commerce_Management_System.Application.OrderReturns
{
    public interface IOrderReturnService
    { 
        Task AddAsync(OrderReturn orderReturn);
        Task<OrderReturn> GetByOrderIdAsync(int orderId);
        Task<OrderReturnPagedListViewModel> GetAllAsync(OrderReturnFilterViewModel filter,int currencyId);
    }
}
