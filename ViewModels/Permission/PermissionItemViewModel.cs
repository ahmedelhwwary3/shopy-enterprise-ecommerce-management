using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Permission
{
    public class PermissionItemViewModel
    {
        public string? Code { get; set; }
        public string? Description { get; set; }

        [Required]
        public enPermissions Id { get; set; }

        [Required]
        public bool IsGranted { get; set; } = false;
    }
}
