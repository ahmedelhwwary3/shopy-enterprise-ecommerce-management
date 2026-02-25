using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.Results;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider;

namespace Enterprise_E_Commerce_Management_System.Application.ShippingProviders
{
    public interface IShippingProviderService
    {
        Task<List<ShippingProviderIdNameViewModel>> GetAllViewModelAsync();
        Task<ShippingProviderPagedListViewModel> GetListViewModelAsync(ShippingProviderFilterViewModel filter);
        Task<ShippingProviderFormViewModel> GetFormViewModelAsync(int? ShippingProviderId=null);
        Task<bool> CheckUniqueNameAsync(string Name, int Id = 0);
        Task<enUpdateShippingProviderResult> UpdateAsync(ShippingProviderFormViewModel viewModel); 
        Task<enCreateShippingProviderResult> CreateAsync(ShippingProviderFormViewModel viewModel); 
        Task<enChangeShippingProviderStatusResult> ChangeActivityStatusById(int ShippingProviderId);
    }
}
