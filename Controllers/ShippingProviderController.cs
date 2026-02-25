using Enterprise_E_Commerce_Management_System.Application.ShippingProviders;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.Results;
using Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    [Authorize(Policy = "ShippingProviders.Manage")]
    public class ShippingProviderController : Controller
    {
        private readonly IShippingProviderService _shippingProviderService;
        public ShippingProviderController(IShippingProviderService shippingProviderService)
        {
            _shippingProviderService = shippingProviderService;
        }
        public async Task<IActionResult> Index(ShippingProviderPagedListViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (viewModel.Filter == null)
                viewModel.Filter = new ShippingProviderFilterViewModel();

            viewModel = await _shippingProviderService.GetListViewModelAsync(viewModel.Filter);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Form(int? ShippingProviderId)
        {
            var viewModel = new ShippingProviderFormViewModel();
            if (ShippingProviderId.HasValue)//Edit
            {
                if (ShippingProviderId.Value <= 0)
                    return BadRequest();

                viewModel = await _shippingProviderService.GetFormViewModelAsync(ShippingProviderId.Value);
                return viewModel == null ? NotFound() : View("Edit", viewModel);
            }
            else //Create
            {
                viewModel = await _shippingProviderService.GetFormViewModelAsync();
                return viewModel == null ? NotFound() : View("Create", viewModel);
            } 
        }

        [HttpGet]
        public async Task<IActionResult> CheckUniqueName(string Name, int Id)
        {
            bool IsUnique = await _shippingProviderService.CheckUniqueNameAsync(Name,Id);
            return Json(IsUnique);
        }

        [HttpPost]
        public async Task<IActionResult> Form(ShippingProviderFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            } 

            enTempMessage message;
            if (viewModel.Id.HasValue)//Edit
            {
                enUpdateShippingProviderResult result = await _shippingProviderService.UpdateAsync(viewModel);
                if (result == enUpdateShippingProviderResult.ProviderNotFound)
                    return NotFound("Shipping provider not found.");

                if (result == enUpdateShippingProviderResult.NotUpdateMode)
                    return NotFound("Invalid data.");
                //success
                message = enTempMessage.UpdatedSuccessfully;
            }
              
            else//Add
            {
                enCreateShippingProviderResult result = await _shippingProviderService.CreateAsync(viewModel);
                if(result==enCreateShippingProviderResult.NotCreateMode)
                    return NotFound("Invalid data.");
                //success
                message = enTempMessage.CreateSucceeded;
            } 

            this.SendTempMessage(message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeActivityStatus(int ShippingProviderId)
        {
            if (ShippingProviderId <= 0)
                return BadRequest();

            enChangeShippingProviderStatusResult result = 
                await _shippingProviderService.ChangeActivityStatusById(ShippingProviderId); 
            return result==enChangeShippingProviderStatusResult.Success ? RedirectToAction(nameof(Index)) 
                : NotFound("Shipping provider not found.");
        }
    }
}
