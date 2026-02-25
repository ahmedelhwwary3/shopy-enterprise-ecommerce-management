using Enterprise_E_Commerce_Management_System.Models.OrderReturns;

namespace Enterprise_E_Commerce_Management_System.ViewModels.OrderReturn 
{
    public class OrderReturnItemViewModel
    {
        public int OrderReturnId { get; set; }
        public string CourierUserName { get; set; }
        public string Notes { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public enOrderReturnStatus ReturnStatus { get; set; }
        public string CountryName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }
}
