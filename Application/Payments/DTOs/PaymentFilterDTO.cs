using Enterprise_E_Commerce_Management_System.ViewModels.Country;

namespace Enterprise_E_Commerce_Management_System.Application.Payments.DTOs
{
    public class PaymentFilterDTO
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
        public int? CountryId { get; set; } 
        public string? Search { get; set; } 
        public enPaymentStatus? PaymentStatus { get; set; }  
        public enPaymentMethod? PaymentMethod { get; set; }  
    }
         
}        
            
     
      