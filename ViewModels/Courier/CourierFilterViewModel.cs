using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Courier
{
    public class CourierFilterViewModel
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;

        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        public List<CountryNameIdViewModel>? Countries { get; set; } 
        public string? Search { get; set; }
        public bool? Status { get; set; }
    }
}
