namespace Enterprise_E_Commerce_Management_System.Application.Carts.DTOs
{
    public class CartDetailsDTO
    {
        public int Id { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalPrice { get; set; } 
        public string Code { get; set; }
        public List<CartItemDTO> Items { get; set; }
       
    }
}
