using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Courier
{
    public class CourierItemViewModel
    {

        public int CourierId { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string CountryName { get; set; }
        public int AllShipments { get; set; }
        public int ReturnedOrders { get; set; }
        public int ShippedOrders { get; set; }
        public int PendingForDelivery { get; set; }
        public int ShippingFailed { get; set; }
        public int CancelledOrders { get; set; }
    }
}
