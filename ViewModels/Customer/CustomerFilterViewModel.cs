using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Customer
{
    public class CustomerFilterViewModel
    { 
        public int Page { get; set; } = ValidationConstants.DefaultPage;
        public int PageSize { get; set; } = ValidationConstants.DefaultManagePageSize;

        [Range(1, int.MaxValue)]
        [Display(Name ="Country")]
        public int? CountryId { get; set; } = null;
        public string? Search { get; set; } = null;
        public bool? IsActive { get; set; } = null;
    }
}
