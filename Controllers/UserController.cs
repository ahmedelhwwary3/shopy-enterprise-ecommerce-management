 
using Enterprise_E_Commerce_Management_System.Application.Users.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Users;
using Microsoft.AspNetCore.Mvc; 
using Enterprise_E_Commerce_Management_System.ViewModels.User;
using System.Threading.Tasks;
using Enterprise_E_Commerce_Management_System.Application.Roles;
using Microsoft.AspNetCore.Identity;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.AspNetCore.Authorization;
using Enterprise_E_Commerce_Management_System.Application.Users.Results;

namespace Enterprise_E_Commerce_Management_System.Controllers
{


    public class UserController : Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly IApplicationRoleService _roleSerivce;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IApplicationUserService userService
            , IApplicationRoleService roleService,
            SignInManager<ApplicationUser> signInManager)
        {
            _userService= userService; 
            _roleSerivce= roleService;
            _signInManager= signInManager;
        }

        [HttpGet]
        [Authorize(Policy = "Users.Manage")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Users.Manage")]
        public async Task<IActionResult> Search(UserFilterDTO filter)
        {
            var listVM = await _userService.GetListViewModelAsync(filter);
            return Json(listVM);
        }

        [HttpGet]
        [Authorize(Policy = "Users.Manage")]
        public async Task<IActionResult> EditPermissions(string UserId)
        {
            var permissionList = await _userService
                .GetUpdatePermissionViewModelAsync(UserId);

            if (permissionList == null)
                return NotFound();

            return View(permissionList);
        }

        [HttpPost]
        [Authorize(Policy = "Users.Manage")]
        public async Task<IActionResult> EditPermissions(UpdateUserPermissionsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var permissionList = (await _userService
                    .GetUpdatePermissionViewModelAsync(viewModel.UserId)).Permissions;
                viewModel.Permissions = permissionList;
                return View(viewModel);
            }

            enUpdateUserPermissionsResult result = await _userService.UpdatePermissionsAsync(viewModel);
            if (result==enUpdateUserPermissionsResult.Success)
            {
                this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
                return RedirectToAction(nameof(Index));
            }

            return NotFound("User not found.");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User is not authenticated.");

            var viewModel = await _userService.GetUpdateProfileViewModelAsync(userId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(UpdateUserProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            enUpdateUserProfileResult result = await _userService.UpdateProfileAsync(viewModel); 

            if (result==enUpdateUserProfileResult.IdentityError||
                result == enUpdateUserProfileResult.InvalidData)
            {
                return BadRequest("Invalid data.");
            }

            if (result == enUpdateUserProfileResult.UserNotFound)
            {
                return NotFound("User not found.");
            }

            var userDB = await _userService.GetUserByNameAsync(viewModel.UserName);
            if (userDB == null)
                return NotFound("User not found.");

            bool IsCourier = await _userService.IsInRoleAsync(userDB, enUserRole.Courier);
            if (IsCourier)
            {
       
                var oldCountryIdClaim = User.FindFirst(storage.CountryIdKey);
                var newCountryIdClaim = new Claim(storage.CountryIdKey, viewModel.CountryId.ToString());
                enUpdateUserClaimsResult claimResult = 
                    await _userService.UpdateClaimAsync(userDB, oldCountryIdClaim, newCountryIdClaim);
                if (claimResult == enUpdateUserClaimsResult.IdentityError||
                    claimResult == enUpdateUserClaimsResult.InvalidData)
                    return BadRequest("Invalid data.");
            }

            await _signInManager.RefreshSignInAsync(userDB);//refresh cookies from Data Base
            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            return RedirectToAction("Index", "Shopping");
        }

        [HttpGet]
        [Authorize(Policy = "Users.Manage")]
        public async Task<IActionResult> Manage(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
                return BadRequest("User Id is required.");

            var viewModel = await _userService.GetUserViewModelAsync(UserId); 
            if (viewModel == null)
                return NotFound();

            if (viewModel.RoleName == enUserRole.Courier.ToString())
                return BadRequest();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(ManageUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.RoleList = await _roleSerivce.GetAllAsync();
                return View(viewModel);
            }

            enUpdateUserResult result = await _userService.UpdateUserAsync(viewModel);
            if (result == enUpdateUserResult.IdentityError)
                return BadRequest("Invalid data.");

            if (result == enUpdateUserResult.UserNotFound)
                return NotFound("User not found.");

            if (result == enUpdateUserResult.NotUniqueName)
                return BadRequest("Not unique name.");  

            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            return RedirectToAction("Index","Shopping");
        }

        [HttpGet] 
        [Authorize]//Logged In
        public IActionResult ChangePassword(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
                return BadRequest("User Id is required.");

            var viewModel = _userService.GetChangePasswordViewModel(UserId);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]//Logged In
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordViewModel viewModel)
        {
            enChangeUserPasswordResult result = await _userService.ChangePasswordAsync(viewModel);
            if(result==enChangeUserPasswordResult.IdentityError)
                return BadRequest("Invalid data.");

            if (result == enChangeUserPasswordResult.UserNotFound)
                return NotFound("User not found."); 

            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet] 
        public async Task<IActionResult> CheckValidPassword(string OldPassword,string Id)
        {
            enCheckValidUserPasswordResult result = await _userService.CheckValidPasswordAsync(OldPassword, Id); 
            return Json(result==enCheckValidUserPasswordResult.Valid?true:false);
        } 
    }
}
