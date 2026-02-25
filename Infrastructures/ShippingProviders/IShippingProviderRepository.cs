using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.ShippingProviders;
using Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.ShippingProviders
{
    public interface IShippingProviderRepository : IRepository<ShippingProvider>
    {
        Task<ICollection<ShippingProviderNameIdDTO>> GetAllAsync();
        Task<ShippingProviderPagedListDTO> GetListDtoAsync(ShippingProviderFilterDTO filter);
        Task<bool> ExistsByNameAsync(string Name);
        Task<ShippingProvider> GetTrackedByNameAsync(string Name);


    }
}
