using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.User
{
    public class ChangeUserPasswordViewModel
    {
        [Required]
        public string Id { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        [Remote("CheckValidPassword", "User",
            ErrorMessage = "Wrong password.",
            AdditionalFields = nameof(Id))]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        [RegularExpression(
         @"^(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*\d)[A-Za-z\d+=!@#$%^&*()_-]{6,20}$",
         ErrorMessage = "Password must be 6-20 characters and contain letters" +
         ", numbers, and at least one uppercase letter.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        [Remote("MatchPassword", "Account",
            ErrorMessage = "Passwords are not matched.",
            AdditionalFields = nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

         
    }
}
