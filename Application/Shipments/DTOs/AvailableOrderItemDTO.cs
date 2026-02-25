namespace Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs
{
    public class AvailableOrderItemDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; } 
        public string City { get; set; }
        public bool IsReturn { get; set; }
    }
}
