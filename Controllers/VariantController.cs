using AutoMapper;
using Enterprise_E_Commerce_Management_System.ViewModels.Variant;
using Microsoft.AspNetCore.Mvc;
using Enterprise_E_Commerce_Management_System.Application.Variants.DTOs;
using Microsoft.AspNetCore.Authorization; 
using Enterprise_E_Commerce_Management_System.Application.Variants;
using Enterprise_E_Commerce_Management_System.Application.Variants.Results;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using Enterprise_E_Commerce_Management_System.Application.Products.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Attributes;

namespace Enterprise_E_Commerce_Management_System.Controllers
{
    [Authorize(Policy = "Variants.Manage")]
    public class VariantController : Controller
    {
        private readonly IVariantService _variantService;
        public VariantController(IVariantService variantService)
        {
            _variantService = variantService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(VariantFilterDTO filter)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            var viewModel = await _variantService
                .GetListByProductIdAsync(filter, currencyId);
            if (viewModel == null)
                return NotFound(); 

            return PartialView("_ShowProductVariants", viewModel);
        }
        /// <summary>
        /// [Remote()] Action for better UX
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CheckSkuUnique(string sku,int? Id=null)
        {
            bool isUnique=await _variantService
                .CheckSkuUniqueAsync(sku,variantId: Id);

            return Json(isUnique);
        }
         
        [HttpGet]
        public async Task<IActionResult>Form(int? Id)
        {
            int? productId = storage.GetInt32FromCookies(
                storage.ProductIdKey, HttpContext);
            if (!productId.HasValue)
                return NotFound("Looks like this page expired." +
                    " Please go back and try again.");
            if (productId.Value < 1)
                return BadRequest();
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            ViewBag.CurrencyId=currencyId;
            var viewModel = new VariantFormViewModel();
            if (Id.HasValue)//Edit (VariantId)
            {
                if (Id.Value <= 0)
                    return BadRequest();

                viewModel = await _variantService
                    .GetVariantFormViewModelAsync(productId.Value, currencyId, Id);
                return View("Edit", viewModel);
            }

            //Create
            viewModel = await _variantService
               .GetVariantFormViewModelAsync(productId.Value, currencyId);
            if (viewModel == null)
                return NotFound();

            return View("Create", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Form(VariantFormViewModel viewModel)
        { 
            if (!ModelState.IsValid)
            {
                return viewModel.Id.HasValue?
                    View("Edit", viewModel): View("Create", viewModel);
            }

            int? ProductId = storage.GetInt32FromCookies(storage.ProductIdKey, HttpContext);
            int currencyId = storage.GetCookieCurrencyIdOrDefault(HttpContext);
            viewModel.ProductId = ProductId.Value;

            if (viewModel.Id.HasValue)//Edit
            {
                if (!ModelState.IsValid)
                    return View("Edit", viewModel);

                enUpdateVariantResult result = await _variantService.UpdateAsync(viewModel, currencyId);
                switch (result)
                { 
                    case enUpdateVariantResult.NotUniqueSKU:
                        {
                            return BadRequest("Not unique sku.");
                        }
                    case enUpdateVariantResult.NotUniqueAttributes:
                        {
                            return NotFound("Not unique attributes.");
                        }
                    case enUpdateVariantResult.VariantNotFound:
                        {
                            return NotFound("Variant not found.");
                        } 
                    case enUpdateVariantResult.Success:
                        {
                            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
                            break;
                        }

                } 

                if (ProductId.HasValue)
                    return RedirectToAction(controllerName: "Product",
                      actionName: "Form", routeValues: new { Id = ProductId.Value });

                return RedirectToAction(controllerName: "Product",
                    actionName: "Index", routeValues: new ProductFilterDTO());
            }
            else//Add
            {
                enCreateVariantResult result = await _variantService.CreateAsync(viewModel, currencyId);
                switch (result)
                { 
                    case enCreateVariantResult.NotUniqueSKU:
                        {
                            return BadRequest("Not unique sku.");
                        }
                    case enCreateVariantResult.NotUniqueAttributes:
                        {
                            return NotFound("Not unique attributes.");
                        }
                    case enCreateVariantResult.Success:
                        {
                            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
                            break;
                        }
                } 
                if (ProductId.HasValue)
                    return RedirectToAction(controllerName: "Product",
                      actionName: "Form", routeValues: new { Id = ProductId.Value });

                return RedirectToAction(controllerName: "Product",
                   actionName: "Index", routeValues: new ProductFilterDTO());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id) 
        {
            if (Id <= 0)
                return BadRequest();

            enDeleteVariantResult result = await _variantService.SoftOrHardDeleteAsync(Id);
            if(result==enDeleteVariantResult.VariantNotFound)
                return NotFound("Variant not found.");
            //success
            this.SendTempMessage(enTempMessage.DeletedSuccessfully);
            return RedirectToAction(controllerName: "Product",
                actionName: "Index", routeValues: new ProductFilterDTO());
        }
    }
}
