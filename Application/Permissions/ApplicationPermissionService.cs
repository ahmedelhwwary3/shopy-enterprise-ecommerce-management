using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Permissions.DTOs;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.ViewModels.Permission;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Application.Permissions
{
    public class ApplicationPermissionService : IApplicationPermissionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ApplicationPermissionService(IUnitOfWork uow,IMapper mapper)
        {
            _mapper=mapper;
            _uow = uow;
        }
        public async Task<List<PermissionItemViewModel>> GetActiveListAsync()
        {
            var listDTO = await _uow.ApplicationPermissions.GetAllActiveListAsync();
            var listVM=_mapper.Map<List<PermissionItemViewModel>>(listDTO);
            return listVM;
        }
        public async Task<ApplicationPermission> GetByIdAsync(int Id)
        {
            return await _uow.ApplicationPermissions
                .GetByIdAsync(Id);
        }
        public async Task<List<enPermissions>> GetGrantedIdListByRoleNameAsync(enUserRole role)
        {
            return await _uow.ApplicationPermissions
                 .GetGrantedIdListByRoleNameAsync(role.ToString());
        }
        public async Task<List<enPermissions>> GetGrantedIdListByRoleIdAsync(string Id)
        {
            return await _uow.ApplicationPermissions
                 .GetGrantedIdListByRoleIdAsync(Id);
        }
        public async Task<List<PermissionItemViewModel>>
            GetAllViewModelAsync()
        {
            var dto= await _uow.ApplicationPermissions.GetAllListDtoAsync(); 
            var viewModel = _mapper.Map<List<PermissionItemViewModel>>(dto);
            return viewModel;
        }
    }
}
