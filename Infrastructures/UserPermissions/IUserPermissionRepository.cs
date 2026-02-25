 
using Enterprise_E_Commerce_Management_System.Application.Permissions.DTOs;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Enterprise_E_Commerce_Management_System.ViewModels.User;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.UserPermissions;

public interface IUserPermissionRepository : IRepository<ApplicationUserPermission> 
{
    Task<List<enPermissions>> GetPermissionIdListByUserIdAsync(string userId);
    Task<List<ApplicationUserPermission>> GetPermissionListByUserNameAsync(string userName);
    Task<List<ApplicationUserPermission>> GetPermissionListByUserIdAsync(string userId);
}
