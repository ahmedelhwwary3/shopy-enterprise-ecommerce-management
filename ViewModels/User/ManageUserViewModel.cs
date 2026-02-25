using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.User
{
    public class ManageUserViewModel
    { 
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "*")]
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
            AdditionalFields =nameof(Id))]
        public string UserName { get; set; }


        public List<RoleItemViewModel>? RoleList { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "*")]
        public string RoleName { get; set; }
    }
}
