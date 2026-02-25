using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using Enterprise_E_Commerce_Management_System.ViewModels.Currency;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Variant
{
    public class VariantFormViewModel
    {
        [Range(1, int.MaxValue,
            ErrorMessage = "Invalid Variant Id.")]
        public int? Id { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Invalid Product Id.")]
        public int ProductId { get; set; }

        [Range(typeof(decimal),
            ValidationConstants.MinPrice,
            ValidationConstants.MaxPrice,
            ErrorMessage = "Invalid Price.")]
        public decimal Price { get; set; }

        [Range(typeof(decimal),
           ValidationConstants.MinPrice,
           ValidationConstants.MaxPrice,
            ErrorMessage = "Invalid Cost.")]
        public decimal Cost { get; set; }

        [Range(ValidationConstants.MinQuantity
          , ValidationConstants.MaxQuantity,
            ErrorMessage = "Invalid Stock Quantity.")]
        public int StockQuantity { get; set; }

        [Remote(action: "CheckSkuUnique",
            controller: "Variant", AdditionalFields = nameof(Id),
             HttpMethod = "GET",
            ErrorMessage = "SKU must be unique.")]
        [Required(ErrorMessage = "*")]
        public string SKU { get; set; }

        [Required]
        public List<VariantAttributeSelectItemViewModel> Attributes { get; set; } 
        public List<CurrencyCodeIdItemViewModel>? Currencies { get; set; } 
        public string? Code { get; set; }
    }
}
