using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Countries;
using Microsoft.AspNetCore.Identity;

namespace Enterprise_E_Commerce_Management_System.Models.Permissions
{
    public class ApplicationUser:IdentityUser
    {
        public override string Id { get => base.Id; set => base.Id = value; } 
        public override string? Email { get => base.Email; set => base.Email = value; } 
        public override string? PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        public override string? UserName { get => base.UserName; set => base.UserName = value; }


        public bool IsActive { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<ApplicationUserPermission> UserPermissions { get; set; }
            = new HashSet<ApplicationUserPermission>();


    }
}
