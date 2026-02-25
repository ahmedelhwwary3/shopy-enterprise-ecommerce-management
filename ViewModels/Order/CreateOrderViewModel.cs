using Enterprise_E_Commerce_Management_System.ViewModels.Customer;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        //public int CartId { get; set; }//Session+Cookies Responsibility (Identity)
        [Required]
        public FullNameViewModel FullName { get; set; }

        [Required]
        public AddressViewModel Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(15, MinimumLength = 10)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }
        [Range(1,int.MaxValue,
            ErrorMessage ="Invalid Country Id.")]
        public int CountryId { get; set; }
    }
}
