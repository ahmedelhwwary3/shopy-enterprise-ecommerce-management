using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Category;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.UserProducts
{
    public class ProductPageViewModel
    {
        [Required]
        public List<CategoryNameIdItemViewModel> Categories {  get; set; }

        //[Required]
        //public List<ProductStatusViewModel> StatusList { get; set; }
    }
}
