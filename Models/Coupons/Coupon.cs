using Enterprise_E_Commerce_Management_System.Models.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.Coupons
{
    public class Coupon
    {
        public int Id {  get; set; }

        [Required]
        public string Code { get; set; }

        public decimal DiscountValue {  get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public enDiscountType DiscountType { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
           = new HashSet<Order>();

    }
}
