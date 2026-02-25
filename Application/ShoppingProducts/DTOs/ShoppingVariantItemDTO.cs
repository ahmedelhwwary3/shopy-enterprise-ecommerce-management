using Enterprise_E_Commerce_Management_System.Application.Attributes.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs
{
    public class ShoppingVariantItemDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public bool IsActive { get; set; } = true;
        public List<VariantAttributeShoppingItemDTO> Attributes { get; set; }
    }
}
