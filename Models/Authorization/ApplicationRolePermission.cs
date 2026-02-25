using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;

namespace Enterprise_E_Commerce_Management_System.Models.Permissions
{
    /// <summary>
    /// Pure Join Table (Composite Key)
    /// </summary>
    public class ApplicationRolePermission
    {
        public enPermissions PermissionId { get; set; }
        public virtual ApplicationPermission Permission { get; set; }


        public string RoleId { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
