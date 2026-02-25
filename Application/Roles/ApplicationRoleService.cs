 
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Enterprise_E_Commerce_Management_System.Application.Permissions;
using Enterprise_E_Commerce_Management_System.Application.RolePermissions;
using Enterprise_E_Commerce_Management_System.Application.UserRoles;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Application.Roles.Results;

namespace Enterprise_E_Commerce_Management_System.Application.Roles
{
    public class ApplicationRoleService:IApplicationRoleService
    { 
        private readonly  IApplicationPermissionService _permissionService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IApplicationRolePermissionService _rolePermService;
        private readonly IApplicationUserRoleService _userRoleService;
        private readonly IUnitOfWork _uow;
        public ApplicationRoleService( 
            IApplicationPermissionService permissionService,
            RoleManager<ApplicationRole> roleManager,
            IApplicationRolePermissionService rolePermService,
            IApplicationUserRoleService userRoleService,
            IUnitOfWork uow)
        { 
            _permissionService=permissionService; 
            _roleManager=roleManager;
            _rolePermService = rolePermService;
            _userRoleService=userRoleService;
            _uow=uow;
        }
        public async Task<string> GetRoleIdByUserId(string userId)
        {
            return await _userRoleService.GetRoleIdByUserId(userId);
        }
        public async Task<string> GetRoleNameByUserIdAsync(string userId)
        {
            return await _userRoleService.GetRoleNameByUserIdAsync(userId);
        }
        public async Task<RoleListViewModel> GetAllWithCountAsync()
        {
            var list= await _roleManager.Roles.Select(a => new RoleItemWithPermissionCountViewModel()
            {
                Name = a.Name.Trim(),
                Id = a.Id,
                PermissionCount = a.RolePermissions.Count
            }).ToListAsync();
            return new RoleListViewModel()
            {
                Roles = list,
                Count = list.Count
            };
        }
        public async Task<List<RoleItemViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles
                .Where(r=>r.Name!=enUserRole.Courier.ToString())
                .Select(a => new RoleItemViewModel()
            {
                Name = a.Name.Trim(),
                Id = a.Id 
            }).ToListAsync();
        }
        public async Task<RoleFormViewModel> GetFormViewModelByIdAsync(string? Id)
        {
            var viewModel = new RoleFormViewModel();
            viewModel.Permissions =
                await _permissionService.GetActiveListAsync();
            if (!string.IsNullOrEmpty(Id))//Edit Only
            {
                var roleDB = await _roleManager.FindByIdAsync(Id);
                if (roleDB == null || string.IsNullOrEmpty(roleDB.Name))
                    return viewModel;//Add

                var allowedPermissions = 
                    await _permissionService.GetGrantedIdListByRoleIdAsync(Id);
                foreach(var p in viewModel.Permissions)
                    p.IsGranted = allowedPermissions.Contains(p.Id);

                viewModel.Name = roleDB.Name;
                viewModel.Id = roleDB.Id;
            } 
            //Edit Or Add
            return viewModel;
        }

        public async Task<enCreateRoleResult> CreateAsync(RoleFormViewModel viewModel)
        {
            if (!string.IsNullOrEmpty(viewModel.Id)||
                string.IsNullOrEmpty(viewModel.Name))
                return enCreateRoleResult.InvalidData;

            //EF will understand that parent PK (RoleId) will be the child fk in (RolePermission Table)
            var roleDB = new ApplicationRole
            {
                Name = viewModel.Name,
                RolePermissions = viewModel.Permissions
                    .Where(p => p.IsGranted)
                    .Select(p => new ApplicationRolePermission
                    {
                        PermissionId = (enPermissions)p.Id
                    })
                    .ToList()
            };
            //This Method Calls  SaveChanges For All Changes
            var result = await _roleManager.CreateAsync(roleDB);

            return result.Succeeded?enCreateRoleResult.Success:enCreateRoleResult.IdentityError;
        } 

        public async Task<enUpdateRoleResult> UpdateAsync(RoleFormViewModel viewModel)//Get Permission For Update (true when has it)
        {
            if (string.IsNullOrEmpty(viewModel.Id) ||
               string.IsNullOrEmpty(viewModel.Name))
                return enUpdateRoleResult.InvalidData;

            var roleDB = await _roleManager.FindByIdAsync(viewModel.Id);
            if (roleDB == null)
                return enUpdateRoleResult.RoleNotFound;

            roleDB.RolePermissions = await _rolePermService
                .GetPermissionListByRoleIdAsync(viewModel.Id);
            ///Compare roleDB With selectedVM to get (toAdd/toRemove) Lists
            //1.Get selectedIdList & roleDbIdList To Enable Comparing with "Contains(Id)"
            var selectedIdList = viewModel.Permissions
                .Where(p => p.IsGranted)
                .Select(p => p.Id)
                .ToList();

            var roleDbIdList = roleDB.RolePermissions
                .Select(rp => rp.PermissionId)
                .ToList();

            //2.Get toAdd List => (Filtered From Selected) [Where] notExists in roleDB 
            var toAddIdList = selectedIdList
                .Where(s => !roleDbIdList.Contains(s))
                .ToList();

            //3.Get toRemove List => (Filtered From roleDB) [Where] notExists in selectedVM
            var toRemoveList = roleDB.RolePermissions
                .Where(db => !selectedIdList.Contains(db.PermissionId))
                .ToList();//Must be Explicit Execusion to avoid modify the same list Exception

            foreach (var Id in toAddIdList)
            {
                roleDB.RolePermissions.Add(new ApplicationRolePermission()
                {
                    PermissionId = Id 
                });
            }
            foreach (var p in toRemoveList)//Composite key must be deleted using Delete , not remove from list (seperate FKs)
            {
                await _uow.ApplicationRolePermissions
                    .DeleteAsync(p.RoleId, p.PermissionId);
            }

            roleDB.Name = viewModel.Name;
            await _roleManager.UpdateAsync(roleDB);
            return enUpdateRoleResult.Success;
        }

        public async Task<enDeleteRoleResult> DeleteByIdAsync(string Id)
        {
            await _uow.BeginTransactionAsync();
            try
            {
                var role = await _roleManager.FindByIdAsync(Id);
                var userRoles = await _userRoleService.GetListByRoleIdAsync(Id);
                //Business Role: No Delete if linked with users 
                if (userRoles.Count > 0)
                    return enDeleteRoleResult.UsersAttachedToRole; 

                //Business Role: delete allowed if linked with permissions
                role.RolePermissions = await _rolePermService
                    .GetPermissionListByRoleIdAsync(Id);
                bool hasPermissions = role.RolePermissions.Count > 0;
                if (hasPermissions)
                {
                    foreach (var rp in role.RolePermissions)
                        await _rolePermService.DeleteAsync(rp.RoleId, rp.PermissionId);
                }

                var result = await _roleManager.DeleteAsync(role); 
                await _uow.CommitAsync();
                return result.Succeeded?enDeleteRoleResult.Success:enDeleteRoleResult.IdentityError;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }
    }
}
