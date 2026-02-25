using Enterprise_E_Commerce_Management_System.ViewModels.Permission;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.User
{
    public class UpdateUserPermissionsViewModel
    {
        [Required] 
        public string UserId { get; set; }

        [Required]
        public List<PermissionItemViewModel> Permissions {  get; set; }
    }
}
