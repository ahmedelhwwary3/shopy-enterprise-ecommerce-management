using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Shipments;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs
{
    public class AssignedShipmentItemDTO
    {
        public int ShipmentId { get; set; }
        public string OrderNumber { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public decimal Amount { get; set; }
        public enOrderStatus OrderStatus { get; set; }
        public enShippingStatus ShipmentStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public bool IsReturn { get; set; }
    }
}
