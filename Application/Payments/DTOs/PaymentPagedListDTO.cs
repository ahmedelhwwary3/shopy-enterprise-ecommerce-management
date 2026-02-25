namespace Enterprise_E_Commerce_Management_System.Application.Payments.DTOs
{
    public class PaymentPagedListDTO
    {
        public int Count { get; set; } 
        public PaymentFilterDTO? Filter { get; set; }
        public List<PaymentItemDTO>? Payments { get; set; }
        public string CurrencyCode { get; set; }
    }
}
