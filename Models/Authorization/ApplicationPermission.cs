using Enterprise_E_Commerce_Management_System.Models.Permissions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles
{
    public class ApplicationPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public enPermissions Id { get; set; }
        public string Code { get; set; }      
        public string Module { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;


        public virtual ICollection<ApplicationRolePermission> RolePermissions { get; set; }
            = new HashSet<ApplicationRolePermission>();
        public virtual ICollection<ApplicationUserPermission> UserPermissions { get; set; }
            =new HashSet<ApplicationUserPermission>();

    }
}
