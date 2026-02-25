using Enterprise_E_Commerce_Management_System.Migrations;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Account
{
    public class RegisterUserViewModel
    {
         

        [EmailAddress]
        [Required(ErrorMessage = "*")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="*")]
        [RegularExpression(
         @"^(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*\d)[A-Za-z\d+=!@#$%^&*()_-]{6,20}$",
         ErrorMessage = "Password must be 6-20 characters and contain letters" +
         ", numbers, and at least one uppercase letter.")] 
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        [Remote("MatchPassword","Account",
            ErrorMessage ="Passwords are not matched.",
            AdditionalFields =nameof(Password))]
        public string ConfirmPassword { get; set; } 

        [Required(ErrorMessage = "*")]
        [StringLength(15, MinimumLength = 10)] 
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required(ErrorMessage = "*")]
        [Display(Name = "User Name")]
        [Remote("CheckUniqueName", "Account",
            ErrorMessage = "This name is taken by another user.")]
        public string UserName { get; set; }


        public List<RoleItemViewModel>? Roles { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage ="*")]
        public string RoleName { get; set; }


        [Range(1, int.MaxValue
            ,ErrorMessage = "Country Id.")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public List<CountryNameIdViewModel>? Countries { get; set; }

    }
}
