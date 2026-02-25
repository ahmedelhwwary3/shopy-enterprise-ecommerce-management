using AutoMapper;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Microsoft.Identity.Client;

namespace Enterprise_E_Commerce_Management_System.Application.UserRoles
{
    public class ApplicationUserRoleService : IApplicationUserRoleService
    {
        private readonly IUnitOfWork _uow;
        public ApplicationUserRoleService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<List<ApplicationUserRole>> GetListByRoleIdAsync(string roleId)
        {
            return await _uow.ApplicationUserRoles.GetListByRoleIdAsync(roleId);
        }
        public async Task<string> GetRoleIdByUserId(string userId)
        {
            return await _uow.ApplicationUserRoles.GetRoleIdByUserId(userId);
        }
        public async Task<string> GetRoleNameByUserIdAsync(string userId)
        {
            return await _uow.ApplicationUserRoles.GetRoleNameByUserIdAsync(userId);
        }
    }
}