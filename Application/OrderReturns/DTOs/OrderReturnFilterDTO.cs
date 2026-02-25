using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;

namespace Enterprise_E_Commerce_Management_System.Application.OrderReturns.DTOs
{
    public class OrderReturnFilterDTO
    {
        public int? CountryId { get; set; } 
        public string? Search { get; set; }
        public enOrderReturnStatus? ReturnStatus { get; set; }
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
    }    
}                
      