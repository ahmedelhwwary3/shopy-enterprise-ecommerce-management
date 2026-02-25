namespace Enterprise_E_Commerce_Management_System.Application.ShippingProviders.DTOs
{
    public class ShippingProviderFilterDTO
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
        public string? Search {  get; set; }
        public bool? IsActive { get; set; }
    }
}
