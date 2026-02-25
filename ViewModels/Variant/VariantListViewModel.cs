using Enterprise_E_Commerce_Management_System.Application.Variants.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Variant
{
    public class VariantListViewModel
    { 
        public int Count { get; set; }
        public string CurrencyCode { get; set; } 
        public List<VariantItemViewModel>? Variants { get; set; }
    }
}
