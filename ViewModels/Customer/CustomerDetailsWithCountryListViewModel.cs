using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Customer
{
    public class CustomerDetailsWithCountryListViewModel
    {
        [Required]
        public FullNameViewModel FullName { get; set; }

        [Required]
        public AddressViewModel Address { get; set; }

        [Required]
        public CustomerDetailsViewModel Details { get; set; }
    }
}
