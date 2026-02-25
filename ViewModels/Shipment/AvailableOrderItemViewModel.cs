using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Shipment
{
    public class AvailableOrderItemViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Order Number")]
        public string OrderNumber { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public decimal Amount { get; set; } 
        public string City { get; set; }
        public string Status { get; set; }
    }
}
