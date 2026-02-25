using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Payments;
using Enterprise_E_Commerce_Management_System.Models.Shipments;

namespace Enterprise_E_Commerce_Management_System.Application.Orders
{
    public class OrderManagementItemDTO
    {
        public int OrderId { get; set; }
        public int CourierId { get; set; }
        public string CourierUserName { get; set; }

        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; } 
        public string City { get; set; }

        public enOrderStatus OrderStatus { get; set; }
        public enShippingStatus ShipmentStatus { get; set; }
        public enPaymentMethod? PaymentMethod { get; set; }
        public enPaymentStatus? PaymentStatus { get; set; }


    }
}
