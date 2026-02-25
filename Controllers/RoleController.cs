 
using Enterprise_E_Commerce_Management_System.Application.Permissions;
using Enterprise_E_Commerce_Management_System.Application.Roles;
using Enterprise_E_Commerce_Management_System.Application.Roles.Results;
using Enterprise_E_Commerce_Management_System.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Enterprise_E_Commerce_Management_System.Controllers
{
    [Authorize(Policy = "Roles.Manage")]
    public class RoleController : Controller
    {
        private readonly IApplicationRoleService _roleService;
        private readonly IApplicationPermissionService _permissionService;
        public RoleController(IApplicationRoleService roleService)
        {
            _roleService= roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listVM =await _roleService.GetAllWithCountAsync();
            if (listVM == null)
                return NotFound();

            return View(listVM);
        }

        [HttpGet]
        public async Task<IActionResult> Form(string? roleId)
        {
            var viewModel = new RoleFormViewModel();
            viewModel = await _roleService.GetFormViewModelByIdAsync(roleId);
            if (viewModel == null)
                return NotFound();

            string viewName = string.IsNullOrEmpty(viewModel.Id) ? "Create" : "Edit";
            return View(viewName, viewModel);
        }

        [HttpPost]  
        public async Task<IActionResult> Form(RoleFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Permissions = 
                    await _permissionService.GetActiveListAsync();
                if (viewModel == null)
                    return NotFound();

                return View(viewModel);
            }   
            if (string.IsNullOrEmpty(viewModel.Id))//Add
            {
                enCreateRoleResult result = await _roleService.CreateAsync(viewModel); 
                if(result==enCreateRoleResult.IdentityError||
                    result==enCreateRoleResult.InvalidData)
                    return BadRequest("Invalid data.");

                if (result == enCreateRoleResult.Success)
                    this.SendTempMessage(enTempMessage.CreateSucceeded); 
            }
            else//Edit
            {
                enUpdateRoleResult result = await _roleService.UpdateAsync(viewModel);
                if (result == enUpdateRoleResult.RoleNotFound)
                    return NotFound("Role not found.");

                if(result == enUpdateRoleResult.InvalidData) 
                return BadRequest("Invalid data.");

                if(result==enUpdateRoleResult.Success)
                    this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            }
            //Success After send TempMessage
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest("Id is required.");

            enDeleteRoleResult result= await _roleService.DeleteByIdAsync(Id);
            if (result==enDeleteRoleResult.UsersAttachedToRole)
                return BadRequest("There are users linked with this role.");

            if (result == enDeleteRoleResult.IdentityError)
                return BadRequest("Invalid data.");
            //Success
            this.SendTempMessage(enTempMessage.DeletedSuccessfully);
            return RedirectToAction(nameof(Index));
        }

    }
}
