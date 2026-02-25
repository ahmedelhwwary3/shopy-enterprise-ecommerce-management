namespace Enterprise_E_Commerce_Management_System.Application.Users.DTOs
{
    public class UserFilterDTO
    {
        public string? Search {  get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }

        public int Page { get; set; } = ValidationConstants.DefaultPage;
        public int? PageSize { get; set; }=ValidationConstants.DefaultManagePageSize;
    }
}
