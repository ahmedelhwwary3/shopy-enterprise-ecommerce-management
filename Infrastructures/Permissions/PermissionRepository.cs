using Enterprise_E_Commerce_Management_System.Application.Permissions.DTOs;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Permissions
{
    public class PermissionRepository : Repository<ApplicationPermission> , IPermissionRepository
    {
        public PermissionRepository(CommerceDbContext context) : base(context) { }
        public async Task<List<PermissionItemDTO>> GetAllActiveListAsync()
        {
            return await _context.ApplicationPermissions
                //.Where(x => x.IsActive) //Global Query Filter Enabled
                .Select(x => new PermissionItemDTO()
                {
                    IsAllowed = false,
                    Code=x.Code,
                    Description=x.Description,
                    Id=x.Id
                })
                .ToListAsync();
        }
        public async Task<List<enPermissions>> GetGrantedIdListByRoleNameAsync(string roleName)
        {
            return await _context.ApplicationRolePermissions
                .Where(rp => rp.Role.Name == roleName)
                .Select(rp=>rp.PermissionId)
                .ToListAsync();
               
        }
        public async Task<List<enPermissions>> GetGrantedIdListByRoleIdAsync(string roleId)
        {
            return await _context.ApplicationRolePermissions
                .Where(rp => rp.RoleId== roleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync();
        }

        public async Task<List<PermissionItemDTO>>
            GetAllListDtoAsync()
        {
            return await _context.ApplicationPermissions 
                .Select(userPerm => new PermissionItemDTO()
                {
                    Code=userPerm.Code,
                    Description=userPerm.Description,
                    Id= userPerm.Id
                }).ToListAsync();
        }
    }
}
