using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Category;
using Enterprise_E_Commerce_Management_System.ViewModels.Currency;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts
{
    public class ShoppingPageViewModel
    {
        [Required]
        public List<CategoryNameIdItemViewModel> Categories { get; set; }

        [Required]
        public List<ProductPriceItemViewModel> ProductPrices { get; set; }

        [Required]
        public List<CurrencyCodeIdItemViewModel> Currencies { get; set; }
        public string Code { get; set; }
    }
}
