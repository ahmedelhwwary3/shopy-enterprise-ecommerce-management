using AutoMapper;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.ViewModels.Permission;
using Enterprise_E_Commerce_Management_System.ViewModels.User;

namespace Enterprise_E_Commerce_Management_System.Application.UserPermissions
{
    public class ApplicationUserPermissionService : IApplicationUserPermissionService
    {
        private readonly IUnitOfWork _uow; 
        public ApplicationUserPermissionService(
            IUnitOfWork uow
            ,IMapper mapper)
        {
            _uow = uow; 
        }
        public async Task<List<enPermissions>>
            GetPermissionIdListByUserIdAsync(string userId)
        {
            var list = await _uow.ApplicationUserPermissions
                .GetPermissionIdListByUserIdAsync(userId);
            return list;
        }

        public async Task<List<ApplicationUserPermission>> GetPermissionListByUserNameAsync(string userName)
        {
            return await _uow.ApplicationUserPermissions.GetPermissionListByUserNameAsync(userName);
        }
        public async Task<List<ApplicationUserPermission>> GetPermissionListByUserIdAsync(string userId)
        {
            return await _uow.ApplicationUserPermissions.GetPermissionListByUserNameAsync(userId);
        }
        public async Task DeleteAsync(int Id)
        {
            await _uow.ApplicationUserPermissions.DeleteByIdAsync(Id);
        }
    }
}
