using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.ViewModels.Permission;

namespace Enterprise_E_Commerce_Management_System.Application.Permissions
{
    public interface IApplicationPermissionService
    {
        Task<List<PermissionItemViewModel>> GetActiveListAsync();
        Task<ApplicationPermission> GetByIdAsync(int Id); 
        Task<List<PermissionItemViewModel>> GetAllViewModelAsync();
        Task<List<enPermissions>> GetGrantedIdListByRoleNameAsync(enUserRole role);
        Task<List<enPermissions>> GetGrantedIdListByRoleIdAsync(string Id);
    }
}
