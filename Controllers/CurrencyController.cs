using Enterprise_E_Commerce_Management_System.Application.Currencies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService=currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var viewModel = await _currencyService.GetAllNameIdViewModelAsync();
            return Json(viewModel);
        }

        [HttpPost]
        public IActionResult SaveChoice(int CurrencyId,string ReturnUrl)
        { 
            storage.RefreshCookie(storage.CurrencyIdKey,CurrencyId,HttpContext);
            return string.IsNullOrEmpty(ReturnUrl) ?
                RedirectToAction("Index", "Shopping") : Redirect(ReturnUrl);
        }
    }
}
