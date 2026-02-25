using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Shipments;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Shipment
{
    public class AssignedShipmentFilterViewModel
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;    
        public int? CourierId { get; set; }
        public string? Search {  get; set; }
        public enShippingStatus? ShipmentStatus { get; set; } = enShippingStatus.AssignedForCourier;
    }
}
