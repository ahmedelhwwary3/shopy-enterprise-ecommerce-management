using Enterprise_E_Commerce_Management_System.Application.OrderReturns;
using Enterprise_E_Commerce_Management_System.ViewModels.OrderReturn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    public class OrderReturnController : Controller
    {
        private readonly IOrderReturnService _orderReturnService;
        public OrderReturnController(IOrderReturnService orderReturnService)
        {
            _orderReturnService= orderReturnService;
        }

        [Authorize(Policy = "OrderReturns.View")]
        public async Task<IActionResult> Index(OrderReturnPagedListViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (viewModel.Filter == null)
                viewModel.Filter = new OrderReturnFilterViewModel();

            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            viewModel = await _orderReturnService.GetAllAsync(viewModel.Filter, currencyId);
            return View(viewModel);
        }
    }
}
