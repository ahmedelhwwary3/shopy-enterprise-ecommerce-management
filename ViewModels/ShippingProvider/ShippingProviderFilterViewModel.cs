using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider
{
    public class ShippingProviderFilterViewModel
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
        public string? Search { get; set; }

        [Display(Name ="Status")]
        public bool? IsActive { get; set; }
    }
}
