using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.UserRoles
{
    public class UserRoleRepository : Repository<ApplicationUserRole> , IUserRoleRepository
    {
        public UserRoleRepository(CommerceDbContext context) : base(context) { }

        public async Task<List<ApplicationUserRole>> GetListByRoleIdAsync(string roleId)
        {
            return await _context.UserRoles
                .Where(userRole => userRole.RoleId == roleId)
                .ToListAsync();
        }
        public async Task<string> GetRoleIdByUserId(string userId)
        {
            //Business Role: User Has Only 1 Role
            return await _context.UserRoles
                .Where(userRole => userRole.UserId == userId)
                .Select(userRole=> userRole.RoleId)
                .FirstOrDefaultAsync();
        }
        public async Task<string> GetRoleNameByUserIdAsync(string userId)
        {
            //Business Role: User Has Only 1 Role
            return await _context.UserRoles
                .Where(userRole => userRole.UserId == userId)
                .Select(userRole => _context.Roles.FirstOrDefault(r=>r.Id== userRole.RoleId).Name)
                .FirstOrDefaultAsync();
        }
    }
}
