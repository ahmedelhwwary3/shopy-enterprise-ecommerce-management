using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs
{
    public class CourierDetailsDTO
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Country { get; set; }
        public string ShippingProvider { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
