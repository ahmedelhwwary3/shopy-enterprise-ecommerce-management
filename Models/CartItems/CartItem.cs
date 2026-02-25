using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Variants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.CartItems
{
    /// <summary>
    /// Specific Copy Of Product Taken From Store Variants Into Cart
    /// </summary>
    public class CartItem
    {
        public int Id {  get; set; }

        public int CartId {  get; set; }
        public virtual Cart Cart {  get; set; }


        [ForeignKey(nameof(Variant))]
        public int VariantId { get; set; }
        public virtual Variant Variant { get; set; }

        public int Quantity { get; set; }
        /// <summary>
        /// SnapShot Of Previous Variant Price
        /// </summary>
        /// 
        public decimal UnitPrice {  get; set; }


        
    }
}
