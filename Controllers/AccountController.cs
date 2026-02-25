 
using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Couriers;
using Enterprise_E_Commerce_Management_System.Application.OrderItems;
using Enterprise_E_Commerce_Management_System.Application.Roles;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders;
using Enterprise_E_Commerce_Management_System.Application.Users;
using Enterprise_E_Commerce_Management_System.Application.Users.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Users.Results;
using Enterprise_E_Commerce_Management_System.Models.Permissions; 
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Enterprise_E_Commerce_Management_System.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Controllers
{ 
    public class AccountController : Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly IApplicationRoleService _roleService;
        private readonly ICountryService _countryService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICourierService _courierService;
        private readonly IShippingProviderService _shippingProviderService;
        public AccountController(
            IApplicationUserService userService,
            IApplicationRoleService roleService,
            SignInManager<ApplicationUser> signInManager,
            ICountryService countryService,
            ICourierService courierService,
            IShippingProviderService shippingProviderService)
        {
            _shippingProviderService=shippingProviderService;
            _userService = userService;
            _roleService = roleService;
            _countryService= countryService;
            _signInManager = signInManager;
            _courierService = courierService;
        }

        [HttpGet]
        [Authorize(Policy = "Account.RegisterUser")]
        public async Task<IActionResult> RegisterUser()
        {
          
            var viewModel = await _userService
                .GetRegisterUserViewModelAsync(); 
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Account.RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Roles = await _roleService.GetAllAsync();
                viewModel.Countries = await _countryService.GeAllViewModelAsync();
                return View(viewModel);
            }

            enRegisterUserResult registerResult = await _userService.RegisterUserAsync(viewModel);
            if (registerResult == null)
                return BadRequest();

            if (registerResult==enRegisterUserResult.InvalidData||
                registerResult == enRegisterUserResult.IdentityError)
            {
                ModelState.AddModelError("", "Invalid data.");
                viewModel.Roles = await _roleService.GetAllAsync();
                viewModel.Countries = await _countryService.GeAllViewModelAsync();
                return View(viewModel);
            }
            //Success
            var userDB = await _userService.GetUserWithPermissionsByNameAsync(viewModel.UserName);
            var claimList = new List<Claim>();
            foreach (var p in userDB.UserPermissions)
            {
                claimList.Add(new Claim(storage.PermissionKey, $"{p.Permission.Code}"));
            }
            enAddUserClaimsResult addClaimResult = await _userService.AddClaimsAsync(userDB, claimList);
            if (addClaimResult==enAddUserClaimsResult.InvalidData||
                addClaimResult == enAddUserClaimsResult.IdentityError)
                return BadRequest("Invalid data.");

            this.SendTempMessage(enTempMessage.CreateSucceeded);
            return RedirectToAction(nameof(Login)); 
        }

        [HttpGet]
        public IActionResult MatchPassword(string Password,string ConfirmPassword)
        {
            bool IsMatch=(Password==ConfirmPassword);
            return Json(IsMatch);
        }

        [HttpGet]
        public async Task<IActionResult> CheckUniqueName(string UserName,string? Id=null)
        {
            bool isUnique = await _userService.CheckUniqueNameAsync(UserName,Id);
            return Json(isUnique);
        }

        [HttpGet]
        [Authorize(Policy = "Account.RegisterCourier")]
        public async Task<IActionResult> RegisterCourier()
        {
            var viewModel = new RegisterCourierViewModel(); 
            viewModel.CountryId = valid.EgyptId; 
            viewModel.Countries = await _countryService.GeAllViewModelAsync();
            viewModel.Providers = await _shippingProviderService.GetAllViewModelAsync();
            return View(viewModel);
        } 

        [HttpPost]
        [Authorize(Policy = "Account.RegisterCourier")]
        public async Task<IActionResult> RegisterCourier(RegisterCourierViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Countries = await _countryService.GeAllViewModelAsync();
                viewModel.Providers = await _shippingProviderService.GetAllViewModelAsync();
                return View(viewModel);
            }

            enRegisterCourierResult regiterResult = await _userService.RegisterCourierAsync(viewModel);
            if (regiterResult==enRegisterCourierResult.IdentityError)
                return BadRequest("Invalid data");
           
            if (regiterResult==enRegisterCourierResult.NotUniqueName)
            {
                ModelState.AddModelError("", "Not unique name.");
                viewModel.Countries = await _countryService.GeAllViewModelAsync();
                viewModel.Providers = await _shippingProviderService.GetAllViewModelAsync();
                return View(viewModel);
            }
            //success
            int courierId = await _courierService.GetIdByUserNameAsync(viewModel.UserName);
            var userDB = await _userService.GetUserWithPermissionsByNameAsync(viewModel.UserName);
            if(userDB==null)
                return NotFound("User not found."); 
            //To Filter Orders in PendingOrders Page 
            var claimList = new List<Claim>() 
            {
                 new Claim( storage.CountryIdKey, $"{viewModel.CountryId}"),
                 new Claim(storage.CourierIdKey, $"{courierId}")
            };
            foreach (var p in userDB.UserPermissions)
            {
                claimList.Add(new Claim(storage.PermissionKey, $"{p.Permission.Code}"));
            }
            enAddUserClaimsResult claimResult = await _userService.AddClaimsAsync(userDB, claimList);
            if (claimResult==enAddUserClaimsResult.IdentityError||
                claimResult == enAddUserClaimsResult.InvalidData)
                return BadRequest("Invalid data.");
            //success
            this.SendTempMessage(enTempMessage.CreateSucceeded);
            return RedirectToAction("Index", "Shopping");
        } 

        [HttpGet] 
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Shopping");
            var viewModel = new LoginViewModel();
            return View(viewModel);
        } 

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            enValidateLoginResult valid = await _userService.ValidateLoginAsync(viewModel);
            if (valid == enValidateLoginResult.InactiveUser)
            {
                ModelState.AddModelError("", "User is inactive, Please connect with admin.");
                return View();
            }
            if (valid == enValidateLoginResult.UserNotFound ||
               valid == enValidateLoginResult.Invalid)
            {
                ModelState.AddModelError("", "User name or password is incorrect.");
                return View(viewModel);
            }

            var proberties = new AuthenticationProperties(); 
            var userDB = await _userService.GetUserByNameAsync(viewModel.UserName); 
            bool IsAdmin =await _userService.IsInRoleAsync(userDB,enUserRole.Admin); 
            if (!IsAdmin)
            {
                proberties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = viewModel.RememberMe,//Session Or Persistence Cookie
                    ExpiresUtc = viewModel.RememberMe ?
                    DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddMinutes(30)
                };
            } 
            else//Admin (More Secured)
            {
                proberties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false,//Session Cookie
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                }; 
            } 

            await _signInManager.SignInAsync(userDB, proberties);//Create Login Cookie (UserId) -
            return RedirectToAction("Index","Shopping");
        }

        [IgnoreAntiforgeryToken]//Not Sensivtive
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Shopping");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
