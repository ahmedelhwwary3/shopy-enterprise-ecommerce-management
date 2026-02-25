
using Enterprise_E_Commerce_Management_System.Application.CartItems;
using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Carts;
using Enterprise_E_Commerce_Management_System.Application.Couriers;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Variants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    [AllowAnonymous]
    public class ShoppingController : Controller
    {
        private readonly IShoppingService _shoppingService;
        private readonly ICourierService _courierService;
        public ShoppingController(IShoppingService shoppingService,ICourierService courierService)
        {
         
            _shoppingService = shoppingService;
            _courierService = courierService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        { 
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            var viewModel = await _shoppingService.GetPageViewModelAsync(currencyId);
            if (viewModel == null)
                return NotFound(); 
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(ShoppingFilterDTO filter)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var viewModel = await _shoppingService.GetProductListAsync(filter);

            return Json(viewModel);
        }
    }
}
