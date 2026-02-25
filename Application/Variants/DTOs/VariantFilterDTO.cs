 
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.Variants.DTOs
{
    public class VariantFilterDTO
    {
         
        public int ProductId {  get; set; }
        public bool? IsActive { get; set; } = true;
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
        public string? SKU { get; set; } = null;
    }
}
