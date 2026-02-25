using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;

namespace Enterprise_E_Commerce_Management_System.Application.Countries
{
    public interface ICountryService
    {
        Task<List<CountryNameIdViewModel>> GeAllViewModelAsync();
    }
}
