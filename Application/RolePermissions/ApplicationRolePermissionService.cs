using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.Permissions;

namespace Enterprise_E_Commerce_Management_System.Application.RolePermissions
{
    public class ApplicationRolePermissionService:IApplicationRolePermissionService
    {
        private readonly IUnitOfWork _uow;
        public ApplicationRolePermissionService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<List<ApplicationRolePermission>> GetPermissionListByRoleIdAsync(string roleId)
        {
           return await _uow.ApplicationRolePermissions.GetPermissionListByRoleIdAsync(roleId);
        }
       
        public async Task<List<enPermissions>> GetPermissionIdListByRoleNameAsync(string roleName)
        {
            return await _uow.ApplicationRolePermissions.GetPermissionIdListByRoleNameAsync(roleName);
        }
        public async Task DeleteAsync(string roleId,enPermissions permissionId)
        {
            await _uow.ApplicationRolePermissions.DeleteAsync(roleId,permissionId);
        }

    }
}
