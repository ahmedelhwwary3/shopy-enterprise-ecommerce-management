using Enterprise_E_Commerce_Management_System.ViewModels.Country;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Payment
{
    public class PaymentFilterViewModel
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
        public int? CountryId { get; set; }
        public List<CountryNameIdViewModel>? CountryList { get; set; }
        public string? Search { get; set; }
        public enPaymentStatus? PaymentStatus { get; set; } = null;
        public enPaymentMethod? PaymentMethod { get; set; } = null;
    }
}
