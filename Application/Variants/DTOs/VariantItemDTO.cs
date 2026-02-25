using Enterprise_E_Commerce_Management_System.Models.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Application.Variants.DTOs
{
    public class VariantItemDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Profit => Price- Cost ;
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public bool IsActive { get; set; }
    }
}
