using Enterprise_E_Commerce_Management_System.Models.Customers;

namespace Enterprise_E_Commerce_Management_System.Application.Customers.DTOs
{
    public class CreateCustomerDTO
    {
        public FullNameDTO FullName { get; set; }
        public AddressDTO Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public enCustomerStatus Status { get; set; }
        //public DateOnly CreatedDate { get; set; }
    }
}
