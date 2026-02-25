namespace Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs
{
    public class CourierFilterDTO
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;

        public int? CountryId { get; set; }
        public string? Search {  get; set; }
        public bool? Status { get; set; }
    }
}
