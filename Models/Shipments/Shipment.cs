using Enterprise_E_Commerce_Management_System.Models.Couriers;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Payments;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.Shipments
{
    public class Shipment
    {
        public int Id {  get; set; }

        public int OrderId {  get; set; }
        public virtual Order Order { get; set; }

        //public string? TrackingNumber {  get; set; }
        public int CourierId { get; set; }
        public virtual Courier Courier { get; set; }

        public enShippingStatus ShipmentStatus { get; set; }

        public DateTime? AssignedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public enOrderStatus OrderStatus { get; set; } = enOrderStatus.InDelivery;

    }
}
