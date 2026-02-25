using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Cart
{
    public class CartItemViewModel
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice {  get; set; }
        public enVariantColor Color { get; set; }
        public enVariantSize Size { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; }
    }
}
