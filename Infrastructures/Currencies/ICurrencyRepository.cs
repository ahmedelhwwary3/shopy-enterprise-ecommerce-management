using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Currencies.DTOs;
using Enterprise_E_Commerce_Management_System.Models;
using Enterprise_E_Commerce_Management_System.Models.CartItems;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Currencies
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        Task<List<CurrencyCodeIdItemDTO>> GetAllNameIdDTOAsync();
        Task<string> GetCodeByIdAsync(int Id);
        Task<decimal> GetDollarRateByIdAsync(int Id);
    }
}
