using Enterprise_E_Commerce_Management_System.Application.CartItems;
using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;
using Enterprise_E_Commerce_Management_System.Application.Carts;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts;
using Enterprise_E_Commerce_Management_System.Application.Variants;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    [AllowAnonymous] 
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IVariantService _variantService;
        private readonly ICartItemService _cartItemService;
        private readonly IShoppingService _shoppingService;
        public CartController(ICartService cartService,
            IVariantService variantService,
            ICartItemService cartItemService,
            IShoppingService shoppingService)
        {
            _cartService = cartService;
            _variantService = variantService;
            _cartItemService = cartItemService;
            _shoppingService = shoppingService;
        }

        private async Task<int> RefreshAndGetItemsCountAsync()
        {
            int? cartId = storage.GetInt32FromSessionOrCookies(storage.CartIdKey, HttpContext);
            int count = await _cartService.GetItemsTotalCountByIdAsync(cartId.Value); 
            if (count < 1)
            {
                storage.RemoveCookie(storage.CartIdKey,HttpContext);
                storage.RemoveSession(storage.CartIdKey, HttpContext);

                storage.RemoveCookie(storage.CartItemsCountKey, HttpContext);
                storage.RemoveSession(storage.CartItemsCountKey, HttpContext);
            }
            else
            { 
                storage.RefreshSession(storage.CartItemsCountKey, count, HttpContext);
                storage.RefreshCookie(storage.CartItemsCountKey, count, HttpContext);
            }
            return count;
        }
         

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            int? cartId = storage.GetInt32FromSessionOrCookies(storage.CartIdKey,HttpContext);
            if (cartId == null) 
                return NotFound("Cart not found.");

            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            var viewModel = await _cartService
                .GetDetailsViewModelByIdAsync(cartId.Value, currencyId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddItem(int Id)
        {
            if (Id <= 0)
                return BadRequest("Invalid Id.");
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext); 
            var viewModel = await _shoppingService
                .GetShoppingtDetailsByProductIdAsync(Id, currencyId);
            if (viewModel == null)
                return NotFound();

            return View(viewModel);
        } 

        [HttpPost]//We Checks in Layout If User Has CartId in Session||Cookies To ViewCart and Place Order
        public async Task<IActionResult> AddItem([FromBody] AddToCartDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //1.Get CustomerId from Cookies & Get CartId from Session (Temp) Or Cookies (Perminent)
            int? sessionCartId = storage.GetInt32FromSession(storage.CartIdKey,HttpContext);
            int? customerId = storage.GetInt32FromCookies(storage.CustomerIdKey,HttpContext);
            //2.If No Cart .. Create With CustomerId If Exists
            if (!sessionCartId.HasValue)//If no Session Check Cookies
            {
                int? cookieCartId = storage.GetInt32FromCookies(storage.CartIdKey,HttpContext);
                if (cookieCartId.HasValue)
                {
                    storage.RefreshSession
                        (storage.CartIdKey, cookieCartId.Value, HttpContext); 
                }
                else
                {
                    sessionCartId = await _cartService.CreateAndGetIdAsync(customerId);
                    var options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(valid.CartCookieDays);
                    storage.RefreshCookieDays(storage.CartIdKey, sessionCartId.Value, 
                        HttpContext, valid.CartCookieDays);
                    storage.RefreshSession(storage.CartIdKey, sessionCartId.Value, HttpContext);
                }
            }
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            decimal unitPrice = await _variantService.GetUnitPriceAsync(dto.VariantId );
            //3.Add Item To Cart
            var result = await _cartItemService
                .CreateAsync(dto, sessionCartId.Value, unitPrice);
            if (result == enCreateCartItemResult.InvalidData)
                return BadRequest();

            await RefreshAndGetItemsCountAsync();
            return Ok(new { Id= sessionCartId.Value});
        }


        [HttpPost]
        public async Task<IActionResult> RemoveItem(int ItemId)
        {
            int? cartId = storage.GetInt32FromSessionOrCookies(storage.CartIdKey, HttpContext);
            if (cartId == null)
                return NotFound("Cart not found.");

            await _cartService.DeleteItemByItemIdAsync(cartId.Value, ItemId);

            int count = await RefreshAndGetItemsCountAsync();
            if (count < 1)
                return RedirectToAction("Index","Shopping"); 
            return RedirectToAction(nameof(Details), new { Id = cartId.Value });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCart()
        {
            int? cartId = storage.GetInt32FromSessionOrCookies(storage.CartIdKey, HttpContext);
            if (cartId == null)
                return NotFound("Cart not found.");
            await _cartService.DeleteByIdAsync(cartId.Value);

            int count = await RefreshAndGetItemsCountAsync();
            if (count < 1)
                return RedirectToAction("Index", "Shopping"); 
      
            this.SendTempMessage(enTempMessage.DeletedSuccessfully);
            return RedirectToAction("Index", "Shopping");
        }

        [HttpGet]
        public async Task<IActionResult> Count()
        {
            int? Id = storage.GetInt32FromSessionOrCookies(storage.CartIdKey, HttpContext);
            if (!Id.HasValue)
                return Json(null);

            int count = await _cartService
                .GetItemsTotalCountByIdAsync(Id.Value);
            return Json(count);
        }

    }
}
