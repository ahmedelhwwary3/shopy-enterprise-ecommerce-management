using Enterprise_E_Commerce_Management_System.Models.Variants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.OrderItems
{
    /// <summary>
    /// Specific Copy Of Product Taken From Store Variants Into Order
    /// </summary>
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId {  get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public int VariantId {  get; set; }

        [Required]
        public virtual Variant Variant { get; set; }

        public int Quantity {  get; set; }
        public decimal Price { get; set; }
     

    }
}
