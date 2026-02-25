using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Microsoft.AspNetCore.Identity;

namespace Enterprise_E_Commerce_Management_System.Models.Permissions
{
    public class ApplicationRole:IdentityRole
    {
        public override string Id { get => base.Id; set => base.Id = value; }
        public override string? Name { get => base.Name; set => base.Name = value; }

        public virtual ICollection<ApplicationRolePermission>RolePermissions { get; set; }
            =new HashSet<ApplicationRolePermission>(); 
    }
}
