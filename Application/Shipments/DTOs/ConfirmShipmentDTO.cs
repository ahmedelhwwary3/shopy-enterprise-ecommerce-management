namespace Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs
{
    public class ConfirmShipmentDTO
    {
        public int ShipmentId { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? PostalCode { get; set; }
        public enShippingStatus LastStatus { get; set; }
        //public enShippingStatus ConfirmStatus { get; set; }
    }
}
