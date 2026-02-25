using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Variant
{
    public class VariantAttributeSelectItemViewModel
    {
        public List<string>? ValueOptions { get; set; }

        //[Range(1, int.MaxValue,
        // ErrorMessage = "Invalid Product Id.")]
        //public int? ProductId { get; set; }

        [Required(ErrorMessage = "*")] 
        public enAttributeName Name {  get; set; }

        [Required(ErrorMessage = "*")]
        public string Value { get; set; }

        public enDisplayType? DisplayType { get; set; }

    }
}
