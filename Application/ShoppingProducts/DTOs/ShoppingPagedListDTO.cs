using Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs
{
    public class ShoppingPagedListDTO
    {
        public List<ShoppingItemDTO> Products { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
    }
}
