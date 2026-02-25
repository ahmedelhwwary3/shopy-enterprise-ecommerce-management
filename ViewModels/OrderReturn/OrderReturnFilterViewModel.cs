using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.OrderReturn 
{
    public class OrderReturnFilterViewModel
    {
        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        public List<CountryNameIdViewModel>? Countries { get; set; }
        public string? Search { get; set; }

        [Display(Name = "Return Status")]
        public enOrderReturnStatus? ReturnStatus { get; set; }
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
    }
}
