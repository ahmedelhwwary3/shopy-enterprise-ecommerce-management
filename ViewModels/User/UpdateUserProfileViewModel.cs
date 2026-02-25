using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.User
{
    public class UpdateUserProfileViewModel
    {
        [Required]
        public string Id { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(15, MinimumLength = 10)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required(ErrorMessage = "*")]
        [Display(Name = "User Name")]
        [Remote("CheckUniqueName", "Account",
            ErrorMessage = "This name is taken by another user.",
            AdditionalFields = nameof(Id))]
        public string UserName { get; set; }

        [Range(1, int.MaxValue,ErrorMessage ="Invalid Country Id.")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public List<CountryNameIdViewModel>? Countries { get; set; }
    }
}
