using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Cart
{
    public class CartDetailsViewModel
    {
        public List<CartItemDTO> Items { get; set; }
        public int TotalCount {  get; set; }
        public decimal TotalPrice {  get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
