using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.Payments
{
    public class Payment
    {
        public int Id {  get; set; }

        [Required]
        public int OrderId {  get; set; }

        [Required]
        public virtual Order Order { get; set; }

        //Denormalization For Simple Queries
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public enPaymentMethod PaymentMethod { get; set; }
        public decimal Amount {  get; set; }
        public enPaymentStatus PaymentStatus { get; set; }
        public DateTime PaymentDate {  get; set; } 

    }
}
