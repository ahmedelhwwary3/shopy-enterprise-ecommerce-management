using Enterprise_E_Commerce_Management_System.ViewModels.Permission;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Role
{
    public class RoleFormViewModel
    {
        /// <summary>
        /// For Edit Mode
        /// </summary>
        [Range(1,int.MaxValue,
            ErrorMessage = "Invalid Id.")]
        public string? Id { get; set; }

        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Required]
        public List<PermissionItemViewModel> Permissions { get; set; }
    }
}
