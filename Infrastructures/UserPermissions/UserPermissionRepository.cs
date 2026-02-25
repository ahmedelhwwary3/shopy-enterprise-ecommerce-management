using Enterprise_E_Commerce_Management_System.Models.Permissions; 
using Microsoft.EntityFrameworkCore; 

namespace Enterprise_E_Commerce_Management_System.Infrastructures.UserPermissions
{
    public class UserPermissionRepository : Repository<ApplicationUserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(CommerceDbContext context) : base(context) { }

        public async Task<List<enPermissions>> GetPermissionIdListByUserIdAsync(string userId)
        {
            return await _context.ApplicationUserPermissions
                .Where(userPerm => userPerm.UserId == userId)
                .Select(userPerm =>userPerm.PermissionId)
                .ToListAsync();
        }

        public async Task<List<ApplicationUserPermission>> GetPermissionListByUserNameAsync(string userName)
        {
            return await _context.ApplicationUserPermissions
                .Where(up => up.User.UserName == userName)
                .Include(up=>up.Permission)
                .ToListAsync();
        }

        public async Task<List<ApplicationUserPermission>> GetPermissionListByUserIdAsync(string userId)
        {
            return await _context.ApplicationUserPermissions
                .Where(up => up.User.Id == userId)
                .Include(up => up.Permission)
                .ToListAsync();
        }
    }
}


