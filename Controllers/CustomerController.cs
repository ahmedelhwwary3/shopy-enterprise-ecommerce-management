using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Customers;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Customers.Mapping;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Global;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Enterprise_E_Commerce_Management_System.Application.Customers.Results;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    [Authorize(Policy = "Customers.Manage")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;
        public CustomerController(ICustomerService customerService,
            ICountryService countryService)
        {
            _customerService = customerService;
            _countryService= countryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(CustomerPagedListViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (viewModel.Filter == null)
                viewModel.Filter = new CustomerFilterViewModel();

             viewModel = await _customerService
                .GetListAsync(viewModel.Filter);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            if (Id <= 0)
                return BadRequest();
            var viewModel = await _customerService.GetDetailsByIdAsync(Id);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Form(int? Id)
        {
            CustomerFormViewModel viewModel;
            if (Id.HasValue)//Edit
            {
                if (Id < 1)
                    return BadRequest();

                viewModel = await _customerService
                    .GetFormViewModelAsync(Id.Value);

            }
            else //Add
            {
                viewModel = await _customerService
                    .GetFormViewModelAsync();
            }
            if (viewModel == null)
                return NotFound();
            return Id.HasValue ? View("Edit", viewModel) :
                 View("Create", viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Form(CustomerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Address.Countries = 
                    await _countryService.GeAllViewModelAsync();
                return viewModel.Id.HasValue ?
                  View("Edit", viewModel) : View("Create", viewModel);
            }

            if (viewModel.Id.HasValue)//Edit
            {
                if (!ModelState.IsValid)
                {
                    viewModel.Address.Countries =
                        await _countryService.GeAllViewModelAsync();
                    return View("Edit",viewModel);
                }
                enUpdateCustomerResult result = await _customerService.UpdateAsync(viewModel);
                if (result == enUpdateCustomerResult.CustomerNotFound)
                    return NotFound("Customer Not Found.");

                this.SendTempMessage(enTempMessage.UpdatedSuccessfully); 
            }
            else //Add
            {
                enCreateCustomerResult result =
                    await _customerService.CreateIfNotExistsAsync(viewModel);
                if (result==enCreateCustomerResult.CustomerExists)
                {
                    ModelState.AddModelError("",
                        "Customer with same email or phone already exists.");
                    viewModel.Address.Countries =
                       await _countryService.GeAllViewModelAsync();
                    return View("Create",viewModel);
                }
                this.SendTempMessage(enTempMessage.CreateSucceeded); 
            }
            //success
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int Id)
        {
            if (Id <= 0)
                return BadRequest();

            enChangeCustomerStatusResult success =
                await _customerService.ChangeCustomerStatusByIdAsync(Id);
            if (success == enChangeCustomerStatusResult.CustomerNotFound)
                return NotFound("Customer not found.");

            //Success
            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
            return RedirectToAction(nameof(Index));
        }
         
    }
}
