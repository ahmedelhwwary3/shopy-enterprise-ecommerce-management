using Enterprise_E_Commerce_Management_System.Models.Shipments;

namespace Enterprise_E_Commerce_Management_System.Application.Orders.DTOs
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public int CourierId { get; set; }

        public string CourierUserName { get; set; }
        public string OrderNumber { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? PostalCode { get; set; }
        public string Country { get; set; }

        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }

        public enOrderStatus OrderStatus { get; set; }
        public enShippingStatus ShipmentStatus { get; set; }
        public enPaymentMethod? PaymentMethod { get; set; }
        public enPaymentStatus? PaymentStatus { get; set; }
    }
}
