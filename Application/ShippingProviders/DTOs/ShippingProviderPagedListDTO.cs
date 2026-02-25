using Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider;

namespace Enterprise_E_Commerce_Management_System.Application.ShippingProviders.DTOs
{
    public class ShippingProviderPagedListDTO
    {
        public ShippingProviderFilterDTO? Filter { get; set; }
        public int Count { get; set; }
        public List<ShippingProviderItemDTO>? Providers { get; set; }
    }
}
