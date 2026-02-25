namespace Enterprise_E_Commerce_Management_System.Application.Users.DTOs
{
    public class UserItemDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int PermissionCount { get; set; } 
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

    }
}
