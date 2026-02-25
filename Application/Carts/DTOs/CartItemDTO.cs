using Enterprise_E_Commerce_Management_System.Application.Attributes.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;

namespace Enterprise_E_Commerce_Management_System.Application.Carts.DTOs
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice {  get; set; } 
        public string ProductName { get; set; }
        public string ImageName  { get; set; }
        public List<VariantAttributeNameValueItemDTO> Attributes { get; set; }

    }
}
