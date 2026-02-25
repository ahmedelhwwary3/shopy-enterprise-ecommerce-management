using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Customers;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Payments
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<bool> ExistsByOrderIdAsync(int orderId);
    }
}
