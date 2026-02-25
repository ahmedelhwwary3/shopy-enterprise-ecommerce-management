using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Customer
{
    public class CustomerFormViewModel
    {
        /// <summary>
        /// For Edit Mode
        /// </summary>
        [Range(1,int.MaxValue,
            ErrorMessage ="Invalid Id.")]
        public int? Id { get; set; }

        [Required]
        public FullNameViewModel FullName { get; set; }

        [Required]
        public AddressViewModel Address { get; set; }

        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(15, MinimumLength = 10)] 
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }  
    }

}
