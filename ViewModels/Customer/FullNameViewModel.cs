using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Customer
{
    public class FullNameViewModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50)]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        public string LastName { get; set; }

        public override string ToString()
        {
            return 
                FirstName + " " +
               (string.IsNullOrEmpty(MiddleName) ? "" : MiddleName + " ") +
               LastName;
        }
    }
}
