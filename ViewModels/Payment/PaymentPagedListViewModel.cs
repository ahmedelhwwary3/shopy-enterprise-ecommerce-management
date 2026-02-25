using Enterprise_E_Commerce_Management_System.Application.Payments.DTOs;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Payment
{
    public class PaymentPagedListViewModel
    {
        public int Count { get; set; }
        public PaymentFilterViewModel? Filter { get; set; }
        public List<PaymentItemViewModel>? Payments { get; set; }
        public string? CurrencyCode { get; set; }
    }
}
