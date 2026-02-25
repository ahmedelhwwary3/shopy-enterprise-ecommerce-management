using Enterprise_E_Commerce_Management_System.Models.Countries;
using Enterprise_E_Commerce_Management_System.Models.Coupons;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.OrderItems;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.Orders
{
    /// <summary>
    /// Order Is The Last SnapShot Of Cart
    /// </summary>
    public class Order
    {
        public int Id {  get; set; }
        /// <summary>
        /// Equals Id After Being Generated Using DB Trigger (After Insert)
        /// </summary>
        public string? OrderNumber {  get; set; }

        /// <summary>
        /// Equals Id After Being Generated Using DB Trigger (After Insert)
        /// </summary>
        public string? AccessToken { get; set; }

        public int CustomerId {  get; set; }
        public virtual Customer Customer { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; } 
        public DateTime OrderDate {  get; set; }
        public decimal TotalAmount { get; set; }
        public enOrderStatus OrderStatus { get; set; }

        public int? CouponId { get; set; }
        public virtual Coupon? Coupon { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
            = new HashSet<Payment>();
        public virtual ICollection<OrderItem> OrderItems { get; set; }
            = new HashSet<OrderItem>(); 
        public virtual ICollection<Shipment>? Shipments { get; set; }
            = new HashSet<Shipment>();

    }
}
