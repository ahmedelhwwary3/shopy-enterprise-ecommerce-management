using Enterprise_E_Commerce_Management_System.Application.Permissions.DTOs;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Carts;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Permissions
{
    public interface IPermissionRepository : IRepository<ApplicationPermission> 
    {
        Task<List<PermissionItemDTO>> GetAllActiveListAsync();
        Task<List<enPermissions>> GetGrantedIdListByRoleNameAsync(string roleName);
        Task<List<enPermissions>> GetGrantedIdListByRoleIdAsync(string roleId);
        Task<List<PermissionItemDTO>> GetAllListDtoAsync();
    }
}
