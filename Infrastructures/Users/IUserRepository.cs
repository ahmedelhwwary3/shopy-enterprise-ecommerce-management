 
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Permissions;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.UserRoles
{
    public interface IUserRepository : IRepository<ApplicationUser> 
    {
        Task<ApplicationUser> GetByIdIncludingInactiveAsync(string userId);
        Task<ApplicationUser> GetIncludingInactiveByNameAsync(string userName);
    }
}
