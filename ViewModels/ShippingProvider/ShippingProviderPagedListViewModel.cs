using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.DTOs;

namespace Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider
{
    public class ShippingProviderPagedListViewModel
    {
        public int Count { get; set; }
        public List<ShippingProviderItemWithCouriersCountViewModel>? Providers { get; set; }
        public ShippingProviderFilterViewModel? Filter { get; set; }
    }
}
