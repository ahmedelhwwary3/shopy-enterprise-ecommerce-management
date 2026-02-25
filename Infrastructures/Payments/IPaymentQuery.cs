using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Payments.DTOs;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Payments
{
    public interface IPaymentQuery
    {
        Task<PaymentPagedListDTO> GetAllListAsync(PaymentFilterDTO filter,int currencyId);
    }
}
