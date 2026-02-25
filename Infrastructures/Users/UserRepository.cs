using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.UserRoles
{
    public class UserRepository : Repository<ApplicationUser> , IUserRepository
    {
        public UserRepository(CommerceDbContext context) : base(context) { }

        public async Task<List<ApplicationUserRole>> GetAllListForRole(string roleId)
        {
            return await _context.UserRoles
                .Where(userRole => userRole.RoleId == roleId)
                .ToListAsync();
        }
        public async Task<ApplicationUser> GetByIdIncludingInactiveAsync(string userId)
        { 
            return await _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u=>u.Id==userId);
        }
        public async Task<ApplicationUser> GetIncludingInactiveByNameAsync(string userName)
        {
            return await _context.Users
               .IgnoreQueryFilters()
               .FirstOrDefaultAsync(u => u.UserName == userName);
        }

    }
}
