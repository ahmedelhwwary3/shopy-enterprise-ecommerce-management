using Enterprise_E_Commerce_Management_System.Application.Roles.Results;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;

namespace Enterprise_E_Commerce_Management_System.Application.Roles
{
    public interface IApplicationRoleService
    {
        Task<RoleListViewModel> GetAllWithCountAsync();
        Task<List<RoleItemViewModel>> GetAllAsync();
        Task<RoleFormViewModel> GetFormViewModelByIdAsync(string? Id);
        Task<enCreateRoleResult> CreateAsync(RoleFormViewModel viewModel); 
        Task<enUpdateRoleResult> UpdateAsync(RoleFormViewModel viewModel);
        Task<enDeleteRoleResult> DeleteByIdAsync(string Id);
        Task<string> GetRoleIdByUserId(string userId);
        Task<string> GetRoleNameByUserIdAsync(string userId);

    }
}
