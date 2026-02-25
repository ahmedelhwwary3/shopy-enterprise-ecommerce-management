using Enterprise_E_Commerce_Management_System.Models.Permissions;

namespace Enterprise_E_Commerce_Management_System.Application.RolePermissions
{
    public interface IApplicationRolePermissionService
    {
        Task<List<ApplicationRolePermission>> GetPermissionListByRoleIdAsync(string roleId); 
        Task<List<enPermissions>> GetPermissionIdListByRoleNameAsync(string roleName);
        Task DeleteAsync(string roleId,enPermissions permissionId);
    }
}
