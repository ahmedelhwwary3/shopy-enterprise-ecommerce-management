

using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;
using Enterprise_E_Commerce_Management_System.ViewModels.Currency;

namespace Enterprise_E_Commerce_Management_System.Application.Currencies
{
    public interface ICurrencyService
    {
        Task<List<CurrencyCodeIdItemViewModel>> GetAllNameIdViewModelAsync();
        Task<string> GetCodeByIdAsync(int Id);
        Task<decimal> GetDollarRateByIdAsync(int Id);
    }
}
