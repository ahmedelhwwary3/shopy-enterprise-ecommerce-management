using Enterprise_E_Commerce_Management_System.Global.Constants;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.RolePermissions
{
    public class RolePermissionRepository : Repository<ApplicationRolePermission> , IRolePermissionRepository
    {
        public RolePermissionRepository(CommerceDbContext context) : base(context) { }
      
        public async Task<List<ApplicationRolePermission>> 
            GetPermissionListByRoleIdAsync(string roleId)
        {
            return await _context.ApplicationRolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<ApplicationRolePermission>GetByCompositeKey(string roleId, enPermissions permissionId)
        {
            return await _context.ApplicationRolePermissions
                .FirstOrDefaultAsync(rp => rp.PermissionId == permissionId && rp.RoleId == roleId);
        }
        public async Task DeleteAsync(string roleId, enPermissions permissionId)
        {
            var entity = await GetByCompositeKey(roleId, permissionId);
            _context.ApplicationRolePermissions.Remove(entity);
        }

        public async Task<List<enPermissions>> GetPermissionIdListByRoleNameAsync(string roleName)
        {
            return await _context.ApplicationRolePermissions
               .Where(rp => rp.Role.Name == roleName)
               .Select(rp=>rp.PermissionId)
               .ToListAsync();
        }
    }
}
