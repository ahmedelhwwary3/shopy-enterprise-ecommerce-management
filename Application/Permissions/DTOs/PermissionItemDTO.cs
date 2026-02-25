namespace Enterprise_E_Commerce_Management_System.Application.Permissions.DTOs
{
    public class PermissionItemDTO
    {
        public enPermissions Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsAllowed { get; set; } = false;
    }
}
