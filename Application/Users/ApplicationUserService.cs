
using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Couriers; 
using Enterprise_E_Commerce_Management_System.Application.Permissions;
using Enterprise_E_Commerce_Management_System.Application.RolePermissions;
using Enterprise_E_Commerce_Management_System.Application.Roles;
using Enterprise_E_Commerce_Management_System.Application.UserPermissions; 
using Enterprise_E_Commerce_Management_System.Application.Users.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Users;
using Enterprise_E_Commerce_Management_System.Models.Couriers;
using Enterprise_E_Commerce_Management_System.Models.Permissions; 
using Enterprise_E_Commerce_Management_System.ViewModels.Account; 
using Enterprise_E_Commerce_Management_System.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Application.Users 
{
    public class ApplicationUserService : IApplicationUserService
    { 
        private readonly UserManager<ApplicationUser> _userManager; 
        private readonly IApplicationRoleService _roleService;
        private readonly IUserQuery _query; 
        private readonly IMapper _mapper;
        private readonly IApplicationRolePermissionService _rolePermService;
        private readonly IApplicationUserPermissionService _userPermissionService;
        private readonly IApplicationPermissionService _permissionService;
        private readonly IUnitOfWork _uow;
        private readonly ICountryService _countryService;
        private readonly ICourierService _courierService;
        public ApplicationUserService(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork uow,
            IApplicationRoleService roleService, 
            IUserQuery query,
            IMapper mapper,
            IApplicationRolePermissionService rolePermService,
            IApplicationUserPermissionService userPermissionService,
            IApplicationPermissionService PermissionService,
            ICountryService countryService,
            ICourierService courierService)
        {
            _courierService = courierService;
            _countryService = countryService;
            _userManager =userManager; 
            _roleService=roleService; 
            _query=query;
            _rolePermService=rolePermService;
            _mapper=mapper;
            _uow=uow;
            _userPermissionService=userPermissionService;
            _permissionService=PermissionService;
        }

        public async Task<RegisterUserViewModel> GetRegisterUserViewModelAsync()
        {
            var viewModel = new RegisterUserViewModel();
            viewModel.Roles = await _roleService.GetAllAsync();
            viewModel.Countries = await _countryService.GeAllViewModelAsync();
            viewModel.CountryId = valid.EgyptId;
            return viewModel;
        }
        
        public async Task<bool> CheckUniqueNameAsync(string userName,string? Id=null)
        {
            if (string.IsNullOrEmpty(userName))
                return false;

            if(!string.IsNullOrEmpty(Id))//Edit
            {
                var userDB = await _uow.ApplicationUsers.GetByIdIncludingInactiveAsync(Id);//Ignore query filter
                if (userDB == null)
                    return false;
                if (userDB.UserName.Trim() == userName.Trim())
                    return true;
            }
            //Add Or Edit
            var user = await _uow.ApplicationUsers.GetIncludingInactiveByNameAsync(userName);//Ignore query filter
            return user == null;
        }
        public async Task<enRegisterUserResult> RegisterUserAsync(RegisterUserViewModel viewModel)
        {
            bool IsUnique = await CheckUniqueNameAsync(viewModel.UserName);
            if (string.IsNullOrEmpty(viewModel.UserName) || !IsUnique)
                return enRegisterUserResult.InvalidData;

            var userDB = new ApplicationUser()
            {
                IsActive = true,
                Email = viewModel.Email,
                UserName = viewModel.UserName,
                PhoneNumber = viewModel.PhoneNumber,
                CountryId = viewModel.CountryId
            };
            //1.Create User
            var createResult= await _userManager
                .CreateAsync(userDB, viewModel.Password);
            if (!createResult.Succeeded)
                return enRegisterUserResult.IdentityError; 

            //2.Add To Default Role Permissions
            var permissions = await _rolePermService
                .GetPermissionIdListByRoleNameAsync(viewModel.RoleName);
            foreach(var p in permissions)
            {
                userDB.UserPermissions.Add(new ApplicationUserPermission()
                {
                    PermissionId=p
                });
            }
            //3.Add to Role
            var addRoleResult = await _userManager
                .AddToRoleAsync(userDB, viewModel.RoleName);
            return addRoleResult.Succeeded?enRegisterUserResult.Success:
                enRegisterUserResult.IdentityError;
        }

        public async Task<UpdateUserProfileViewModel> GetUpdateProfileViewModelAsync(string Id)
        {
            var viewModel = new UpdateUserProfileViewModel(); 
            var userDB = await _userManager.FindByIdAsync(Id);
            if (userDB == null)
                return null;

            viewModel.PhoneNumber = userDB.PhoneNumber;
            viewModel.Email = userDB.Email;
            viewModel.UserName = userDB.UserName;
            viewModel.Id = userDB.Id;
            viewModel.Countries = await _countryService.GeAllViewModelAsync();
            viewModel.CountryId = userDB.CountryId;
            return viewModel;
        }

        public async Task<enUpdateUserProfileResult> UpdateProfileAsync(UpdateUserProfileViewModel viewModel)
        {
            bool IsUnique = await CheckUniqueNameAsync(viewModel.UserName, viewModel.Id);
            if (string.IsNullOrWhiteSpace(viewModel.Id) ||
                string.IsNullOrWhiteSpace(viewModel.UserName) || !IsUnique)
                return enUpdateUserProfileResult.InvalidData;

            var user = await _userManager.FindByIdAsync(viewModel.Id);
            if (user == null)
                return enUpdateUserProfileResult.UserNotFound;

            user.UserName = viewModel.UserName;
            user.Email = viewModel.Email;
            user.PhoneNumber = viewModel.PhoneNumber;
            user.CountryId = viewModel.CountryId;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded ? enUpdateUserProfileResult.Success :
                enUpdateUserProfileResult.IdentityError;
        }
        public async Task<UserPagedListViewModel> GetListViewModelAsync(UserFilterDTO filter)
        {
            var listDTO= await _query.GetListDtoAsync(filter);
            var listVM = _mapper.Map<UserPagedListViewModel>(listDTO);
            return listVM;
        }
        public async Task<UpdateUserPermissionsViewModel>
            GetUpdatePermissionViewModelAsync(string userId)
        {
            var grantedPermissionIdList = await _userPermissionService
                .GetPermissionIdListByUserIdAsync(userId);
            var allSystemPermissions = await _permissionService.GetAllViewModelAsync();

            foreach (var p in allSystemPermissions)
                p.IsGranted = grantedPermissionIdList.Contains(p.Id);

            var viewModel = new UpdateUserPermissionsViewModel()
            {
                Permissions= allSystemPermissions,
                UserId= userId
            };
            return viewModel;
        }

        public async Task<enUpdateUserPermissionsResult> UpdatePermissionsAsync(UpdateUserPermissionsViewModel viewModel)
        {
            var userDB = await _userManager.Users
                .Include(u=>u.UserPermissions)
                .Where(u=>u.Id==viewModel.UserId)
                .FirstOrDefaultAsync();

            if (userDB == null)
                return enUpdateUserPermissionsResult.UserNotFound; 

            var grantedPermissionIdList = viewModel.Permissions
                .Where(p=>p.IsGranted)
                .Select(p=>p.Id);
            var userPermissionIdList= userDB.UserPermissions
                .Select(p => p.PermissionId);

            var toAddPermissionIdList = grantedPermissionIdList
                .Where(Id=>!userPermissionIdList
                .Contains(Id));
            var toRemovePermissionList = userDB.UserPermissions
                .Where(userPerm => !grantedPermissionIdList
                .Contains(userPerm.PermissionId));

            foreach (var Id in toAddPermissionIdList)
                userDB.UserPermissions.Add(new ApplicationUserPermission()
                {
                    PermissionId=Id
                }); 
            foreach (var p in toRemovePermissionList)
            {
                await _userPermissionService.DeleteAsync(p.Id);
            }
            _uow.ApplicationUsers.Update(userDB);
            await _uow.SaveChangesAsync();
            return enUpdateUserPermissionsResult.Success;
        }

        public async Task<ManageUserViewModel> GetUserViewModelAsync(string userId)
        {
            var userDB=await _uow.ApplicationUsers.GetByIdIncludingInactiveAsync(userId);
            if (userDB == null)
                return null;

            var roleName = await _roleService.GetRoleNameByUserIdAsync(userDB.Id);
            var roleList = await _roleService.GetAllAsync();
            var viewModel = new ManageUserViewModel()
            {
                IsActive=userDB.IsActive,
                Email=userDB.Email,
                Id=userDB.Id,
                PhoneNumber=userDB.PhoneNumber,
                UserName=userDB.UserName,
                RoleName=roleName,
                RoleList=roleList
            };

            return viewModel;
        }
        public async Task<enUpdateUserResult> 
            UpdateUserAsync(ManageUserViewModel viewModel)
        {
            if (!await CheckUniqueNameAsync(viewModel.UserName, viewModel.Id))
                return enUpdateUserResult.NotUniqueName;

            var userDB = await _uow.ApplicationUsers.GetByIdIncludingInactiveAsync(viewModel.Id);
            if (userDB == null)
                return enUpdateUserResult.UserNotFound;


            userDB.PhoneNumber = viewModel.PhoneNumber;
            userDB.Email = viewModel.Email;
            userDB.IsActive=viewModel.IsActive;
            userDB.UserName= viewModel.UserName;

            var userRoles = await _userManager.GetRolesAsync(userDB);
            if(userRoles.Any())
            {
                var removeRoleResult = await _userManager.RemoveFromRolesAsync(userDB, userRoles);
                if (removeRoleResult == null || !removeRoleResult.Succeeded)
                    return enUpdateUserResult.IdentityError;
            } 
            var addRoleResult = await _userManager.AddToRoleAsync(userDB, viewModel.RoleName);
            if (addRoleResult == null || !addRoleResult.Succeeded)
                return enUpdateUserResult.IdentityError;
            //Update Courier Status With User Status
            if(viewModel.RoleName==enUserRole.Courier.ToString())
            {
                var courier = await _courierService.GetByUserNameAsync(viewModel.UserName);
                courier.IsActive = viewModel.IsActive;
            } 
            //Single Context Save
            var result= await _userManager.UpdateAsync(userDB);
            return result.Succeeded ? enUpdateUserResult.Success 
                : enUpdateUserResult.IdentityError;
        }
        public ChangeUserPasswordViewModel GetChangePasswordViewModel(string userId)
        {
            var viewModel = new ChangeUserPasswordViewModel();
            viewModel.Id=userId;
            return viewModel;
        }

        public async Task<enChangeUserPasswordResult> ChangePasswordAsync(ChangeUserPasswordViewModel viewModel)
        {
            var userDB=await _uow.ApplicationUsers.GetByIdIncludingInactiveAsync(viewModel.Id);
            if (userDB == null)
                return enChangeUserPasswordResult.UserNotFound;

            var result = await _userManager.ChangePasswordAsync(
                userDB, viewModel.OldPassword, viewModel.Password);
            return result.Succeeded?enChangeUserPasswordResult.Success
                :enChangeUserPasswordResult.IdentityError; 
        }

        public async Task<enCheckValidUserPasswordResult> CheckValidPasswordAsync(string OldPassword, string Id)
        {
            var userDB = await _uow.ApplicationUsers.GetByIdIncludingInactiveAsync(Id);
            if (userDB == null)
                return enCheckValidUserPasswordResult.UserNotFound;
            bool success = await _userManager.CheckPasswordAsync(userDB, OldPassword);
            return success? enCheckValidUserPasswordResult.Valid: enCheckValidUserPasswordResult.Invalid;
        }
         
        public async Task<enValidateLoginResult> ValidateLoginAsync(LoginViewModel viewModel)
        {
            var userDB = await _userManager.FindByNameAsync(viewModel.UserName);
            if (userDB == null)
                return enValidateLoginResult.UserNotFound;

            bool Valid = userDB.IsActive;
            if (!Valid)
                return enValidateLoginResult.InactiveUser;

            Valid = await _userManager.CheckPasswordAsync(userDB, viewModel.Password); 
            return Valid? enValidateLoginResult.Valid: enValidateLoginResult.Invalid; 
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> GetUserWithPermissionsByNameAsync(string userName)
        {
            var userDB= await _userManager.FindByNameAsync(userName);
            if (userName == null)
                return null;

            userDB.UserPermissions=await _userPermissionService
                .GetPermissionListByUserNameAsync(userName);
            return userDB;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user,enUserRole role)
        {
            bool IsInRole = await _userManager.IsInRoleAsync(user, role.ToString());
            return IsInRole;
        }

        public async Task<enRegisterCourierResult> RegisterCourierAsync(RegisterCourierViewModel viewModel)
        {
            if (!await CheckUniqueNameAsync(viewModel.UserName))
                return enRegisterCourierResult.NotUniqueName;
            await _uow.BeginTransactionAsync();
            try
            {
                //1.Create User
                var userDB = new ApplicationUser()
                {
                    IsActive = true,
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    UserName = viewModel.UserName,
                    CountryId = viewModel.CountryId
                };
                var permissionList = await _permissionService
                    .GetGrantedIdListByRoleNameAsync(enUserRole.Courier);
                foreach (var Id in permissionList)
                {
                    userDB.UserPermissions.Add(new ApplicationUserPermission()
                    {
                        PermissionId = Id
                    });
                }
                var createResult = await _userManager.CreateAsync(userDB, viewModel.Password);
                if (!createResult.Succeeded)
                    return enRegisterCourierResult.IdentityError;
                //2.Create Courier
                var courierDB = new Courier()
                {
                    IsActive = true,
                    UserId = userDB.Id,
                    ShippingProviderId = viewModel.ShippingProviderId
                };
                await _courierService.AddAsync(courierDB);
                var result= await _userManager.AddToRoleAsync(userDB, enUserRole.Courier.ToString());
                await _uow.CommitAsync();
                return result.Succeeded?enRegisterCourierResult.Success
                    :enRegisterCourierResult.IdentityError;
            }
            catch
            {
                await _uow.RollbackAsync();
                throw new Exception("Register Courier Failed.");
            }
        }

        public async Task<enAddUserClaimsResult> AddClaimsAsync
            (ApplicationUser user,List<Claim> claims)
        {
            var result = await _userManager.AddClaimsAsync(user, claims);
            return result.Succeeded?enAddUserClaimsResult.Success
                :enAddUserClaimsResult.IdentityError;
        }

        public async Task<enUpdateUserClaimsResult> UpdateClaimAsync(ApplicationUser user,Claim Old, Claim New)
        {
            var result = await _userManager.ReplaceClaimAsync(user, Old, New);
            return result.Succeeded ? enUpdateUserClaimsResult.Success
                : enUpdateUserClaimsResult.IdentityError;

        }

    
    }
}
