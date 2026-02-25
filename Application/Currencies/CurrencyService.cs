using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.ViewModels.Currency;

namespace Enterprise_E_Commerce_Management_System.Application.Currencies
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CurrencyService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper=mapper;
        }

        public async Task<List<CurrencyCodeIdItemViewModel>> GetAllNameIdViewModelAsync()
        {
            var dto= await _uow.Currencies.GetAllNameIdDTOAsync();
            var viewModel = _mapper.Map<List<CurrencyCodeIdItemViewModel>>(dto);
            return viewModel;
        }

        public async Task<string> GetCodeByIdAsync(int Id)
        {
            return await _uow.Currencies.GetCodeByIdAsync(Id);
        }

        public async Task<decimal> GetDollarRateByIdAsync(int Id)
        {
            return await _uow.Currencies.GetDollarRateByIdAsync(Id);
        }
    }
}
