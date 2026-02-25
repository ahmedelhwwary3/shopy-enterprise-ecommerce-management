using Enterprise_E_Commerce_Management_System.ViewModels.Payment;
using Enterprise_E_Commerce_Management_System.Application.Payments.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Enterprise_E_Commerce_Management_System.Application.Payments;
using Microsoft.AspNetCore.Authorization;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService=paymentService;
        }

        [Authorize(Policy = "Payments.View")]
        public async Task<IActionResult> Index(PaymentPagedListViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (viewModel.Filter == null)
                viewModel.Filter = new PaymentFilterViewModel();

            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            viewModel = await _paymentService.GetAllAsync(viewModel.Filter, currencyId);
            return View(viewModel);
        }
    }
}
