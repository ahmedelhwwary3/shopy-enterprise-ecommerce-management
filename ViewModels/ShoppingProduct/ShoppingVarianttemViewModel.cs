using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;

namespace Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts
{
    public class ShoppingVarianttemViewModel
    {
        public int Id { get; set; } 
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public bool IsActive { get; set; } = true; 
        public List<ShoppingVariantAttributeItemViewModel> Attributes { get; set; }
    }
}
