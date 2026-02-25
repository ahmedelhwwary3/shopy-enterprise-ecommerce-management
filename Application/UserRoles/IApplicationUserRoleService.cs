using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;

namespace Enterprise_E_Commerce_Management_System.Application.UserRoles
{
    public interface IApplicationUserRoleService
    {
        Task<List<ApplicationUserRole>> GetListByRoleIdAsync(string roleId);
        Task<string> GetRoleIdByUserId(string userId);
        Task<string> GetRoleNameByUserIdAsync(string userId);
    }
}
