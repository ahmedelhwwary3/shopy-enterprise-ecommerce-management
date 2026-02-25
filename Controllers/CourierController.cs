using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Couriers;
using Enterprise_E_Commerce_Management_System.Application.Couriers.Results;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders;
using Enterprise_E_Commerce_Management_System.ViewModels.Courier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    [Authorize(Policy = "Couriers.Manage")]
    public class CourierController : Controller
    {
        private readonly ICourierService _courierService;
        private readonly IShippingProviderService _shippingProviderService;
        private readonly ICountryService _countryService;
        public CourierController(
            ICourierService courierService,
            IShippingProviderService shippingProviderService,
            ICountryService countryService)
        {
            _courierService= courierService;
            _shippingProviderService= shippingProviderService;
            _countryService= countryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CourierPagedListViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (viewModel.Filter == null)
                viewModel.Filter = new CourierFilterViewModel();
            
            viewModel = await _courierService.GetListAsync(viewModel.Filter);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int CourierId)
        {
            if (CourierId <= 0)
                return BadRequest();

            var viewModel = await _courierService.GetDetailsViewModelByIdAsync(CourierId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        [HttpGet] 
        public async Task<IActionResult> Edit(int CourierId)
        {
            if (CourierId <= 0)
                return BadRequest();

            var viewModel = await _courierService.GetEditViewModelAsync(CourierId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCouierViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Providers = await _shippingProviderService.GetAllViewModelAsync();
                viewModel.Countries = await _countryService.GeAllViewModelAsync();
                return View(viewModel);
            }

            enUpdateCourierResult result = await _courierService.UpdateAsync(viewModel);
            if (result == enUpdateCourierResult.CourierNotFound)
                return NotFound("Courier not found.");

            if (result == enUpdateCourierResult.NotUpdateMode)
                return BadRequest("Invalid data.");
            //Success
            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
