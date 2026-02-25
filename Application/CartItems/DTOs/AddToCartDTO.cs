using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs
{
    public class AddToCartDTO
    {
        [Range(1,int.MaxValue)]
        public int VariantId {  get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
