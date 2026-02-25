using Enterprise_E_Commerce_Management_System.Models.Customers;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Customer
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; } 
        [Display(Name = "Created Date")]

        public DateOnly CreatedDate { get; set; }
        public string Country {  get; set; }
    }
}
