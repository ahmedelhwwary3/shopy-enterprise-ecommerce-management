 
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Permissions;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.RolePermissions
{
    public interface IRolePermissionRepository : IRepository<ApplicationRolePermission> 
    {
        Task<ApplicationRolePermission> GetByCompositeKey(string roleId, enPermissions permissionId);
        Task<List<ApplicationRolePermission>> GetPermissionListByRoleIdAsync(string roleId); 
        Task<List<enPermissions>> GetPermissionIdListByRoleNameAsync(string roleName);
        Task DeleteAsync(string roleId, enPermissions permissionId);
    }
}
