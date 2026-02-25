namespace Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs
{
    public class CourierItemDTO
    {
        public int CourierId { get; set; }
        public bool Status { get; set; }
        public string UserName { get; set; }
        public string CountryName { get; set; }
        public int AllShipments {  get; set; } 
        public int ReturnedOrders { get; set; }
        public int ShippedOrders { get; set; }
        public int PendingForDelivery { get; set; }
        public int ShippingFailed { get; set; } 
        public int CancelledOrders { get; set; }
    }   
}      
         