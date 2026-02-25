 
using Enterprise_E_Commerce_Management_System.Application.Users.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Users.Results;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.ViewModels.Account;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Enterprise_E_Commerce_Management_System.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace Enterprise_E_Commerce_Management_System.Application.Users
{
    public interface IApplicationUserService
    {
        Task<RegisterUserViewModel> GetRegisterUserViewModelAsync();
        Task<enUpdateUserResult> UpdateUserAsync(ManageUserViewModel viewModel); 
        Task<ManageUserViewModel> GetUserViewModelAsync(string userId);
        Task<UpdateUserProfileViewModel> GetUpdateProfileViewModelAsync(string Id);
        Task<enRegisterUserResult> RegisterUserAsync(RegisterUserViewModel viewModel);
        Task<enRegisterCourierResult> RegisterCourierAsync(RegisterCourierViewModel viewModel);
        Task<enUpdateUserProfileResult> UpdateProfileAsync(UpdateUserProfileViewModel viewModel);
        Task<bool> CheckUniqueNameAsync(string userName, string? Id);
        Task<UserPagedListViewModel> GetListViewModelAsync(UserFilterDTO filter);
        Task<UpdateUserPermissionsViewModel> GetUpdatePermissionViewModelAsync(string UserId);
        Task<enUpdateUserPermissionsResult> UpdatePermissionsAsync(UpdateUserPermissionsViewModel viewModel);
        ChangeUserPasswordViewModel GetChangePasswordViewModel(string userId);
        Task<enChangeUserPasswordResult> ChangePasswordAsync(ChangeUserPasswordViewModel viewModel);
        Task<enCheckValidUserPasswordResult> CheckValidPasswordAsync(string OldPassword, string Id);
        Task<enValidateLoginResult> ValidateLoginAsync(LoginViewModel viewModel);
        Task<ApplicationUser> GetUserByNameAsync(string userName);
        Task<bool> IsInRoleAsync(ApplicationUser user,enUserRole role);
        Task<enAddUserClaimsResult> AddClaimsAsync(ApplicationUser user,List<Claim>claims);
        Task<enUpdateUserClaimsResult> UpdateClaimAsync(ApplicationUser user, Claim Old,Claim New);
        Task<ApplicationUser> GetUserWithPermissionsByNameAsync(string userName);
        
    }
}
