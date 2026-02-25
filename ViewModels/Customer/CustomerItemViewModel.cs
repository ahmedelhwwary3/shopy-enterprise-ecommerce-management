using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Customer
{
    public class CustomerItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        [Display(Name = "Create Date")]
        public DateOnly CreateDate { get; set; }
        public string Country { get; set; } 
    }
}
