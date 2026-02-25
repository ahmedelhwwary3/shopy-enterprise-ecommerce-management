using Enterprise_E_Commerce_Management_System.Application.Payments.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Payment;

namespace Enterprise_E_Commerce_Management_System.Application.Payments
{
    public interface IPaymentService
    {
        Task AddAsync(Payment payment);
        Task<PaymentPagedListViewModel> GetAllAsync(PaymentFilterViewModel filter,int currencyId);
        Task<bool> ExistsByOrderIdAsync(int orderId);
    }
}
