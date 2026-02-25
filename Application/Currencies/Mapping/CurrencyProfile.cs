using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Currencies.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Currency;

namespace Enterprise_E_Commerce_Management_System.Application.Currencies.Mapping
{
    public class CurrencyProfile:Profile
    {
        public CurrencyProfile()
        {
            CreateMap<CurrencyCodeIdItemDTO, CurrencyCodeIdItemViewModel>();
        }
    }
}
