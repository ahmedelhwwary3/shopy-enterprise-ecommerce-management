using Enterprise_E_Commerce_Management_System.Models.Orders;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Shipment
{
    public class ShipmentDetailsViewModel
    {
        [Range(1,int.MaxValue, 
            ErrorMessage = "Invalid Id.")]
        public int Id { get; set; }
        public string Status { get; set; }

        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }
        public string Country { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string CurrencyCode { get; set; }
    }
}
