namespace Enterprise_E_Commerce_Management_System.ViewModels.Payment
{
    public class PaymentItemViewModel
    { 
        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        public string FullName { get; set; }
        public string CountryName { get; set; }
        public string PaymentDate { get; set; }
        public enPaymentMethod PaymentMethod { get; set; }
        public enPaymentStatus PaymentStatus { get; set; }
    }
}
