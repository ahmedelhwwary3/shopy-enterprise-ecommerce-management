
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs
{
    public class ShoppingItemDTO
    {
        public int ProductId {  get; set; }
        public string Name { set; get; }
        public string ImageName { set; get; }
        public string CategoryName { set; get; }
        public decimal MinPrice { set; get; }
        public decimal MaxPrice { set; get; }
        public bool IsActive { set; get; } = true;
    }
}
