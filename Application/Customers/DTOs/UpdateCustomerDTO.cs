using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Payments;
using Enterprise_E_Commerce_Management_System.Models.Reviews;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.Customers.DTOs
{
    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
        public FullNameDTO FullName { get; set; }
        public AddressDTO Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public enCustomerStatus Status { get; set; }
        //public DateOnly CreatedDate { get; set; }
    }
}
