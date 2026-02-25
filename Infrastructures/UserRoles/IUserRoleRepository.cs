 
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Permissions;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.UserRoles
{
    public interface IUserRoleRepository : IRepository<ApplicationUserRole> 
    {
        Task<List<ApplicationUserRole>> GetListByRoleIdAsync(string roleId);
        Task<string> GetRoleIdByUserId(string userId);
        Task<string> GetRoleNameByUserIdAsync(string userId);
    }
}
