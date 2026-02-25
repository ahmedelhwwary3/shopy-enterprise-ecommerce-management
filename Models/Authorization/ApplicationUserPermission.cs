using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;

namespace Enterprise_E_Commerce_Management_System.Models.Permissions
{
    /// <summary>
    /// This table has its own Id because it represents behavior (permission override),
    /// not just a pure many-to-many relationship.
    /// </summary>
    public class ApplicationUserPermission
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public enPermissions PermissionId { get; set; }
        public virtual ApplicationPermission Permission { get; set; }

        //public bool IsGranted { get; set; }//Already Known by add or delete permission 
    }
}
