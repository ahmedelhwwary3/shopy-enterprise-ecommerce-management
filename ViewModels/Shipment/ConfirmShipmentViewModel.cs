using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Shipment
{
    public class ConfirmShipmentViewModel
    {
        public int ShipmentId { get; set; }
        public string FullName { get; set; }  
        public string Country {  get; set; }
        public string City { get; set; } 
        public string Street { get; set; } 
        public string? PostalCode { get; set; }
        public enShippingStatus LastStatus { get; set; }
        public List<ShipmentStatusItemViewModel>? ShipmentStatusList { get; set; }
        public enShippingStatus ConfirmStatus { get; set; } = enShippingStatus.Shipped;
    }
}
