
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.ViewModels.Permission;
using Enterprise_E_Commerce_Management_System.ViewModels.User;

namespace Enterprise_E_Commerce_Management_System.Application.UserPermissions
{
    public interface IApplicationUserPermissionService
    {
        Task<List<enPermissions>> GetPermissionIdListByUserIdAsync(string userId);
        Task<List<ApplicationUserPermission>> GetPermissionListByUserNameAsync(string userName);
        Task<List<ApplicationUserPermission>> GetPermissionListByUserIdAsync(string userId);
        Task DeleteAsync(int Id);
    }
}
