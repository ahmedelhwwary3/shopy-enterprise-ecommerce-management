using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs
{
    public class ShipmentDetailsDTO
    {
        public int Id { get; set; }
        public enOrderStatus Status { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? PostalCode { get; set; } 
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string CurrencyCode { get; set; }
    }
}
