 
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs
{
    public class ShoppingtDetailsDTO
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public string Code { get; set; }

        public List<ShoppingVariantItemDTO>? Variants { get; set; } 
    }
}
