using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.AspNetCore.Identity;

namespace Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles
{
    public class ApplicationUserRole:IdentityUserRole<string>
    {
        public override string RoleId { get => base.RoleId; set => base.RoleId = value; }
        public override string UserId { get => base.UserId; set => base.UserId = value; }
    }
}
