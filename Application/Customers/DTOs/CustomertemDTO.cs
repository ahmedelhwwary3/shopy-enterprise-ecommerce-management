using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Customers;

namespace Enterprise_E_Commerce_Management_System.Application.Customers.DTOs
{
    public class CustomerItemDTO
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateDate { get; set; }
        public string Country {  get; set; }
    }
}
