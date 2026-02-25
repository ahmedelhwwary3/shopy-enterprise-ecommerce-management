 
using Enterprise_E_Commerce_Management_System.Application.Categories;
using Enterprise_E_Commerce_Management_System.Application.Products;
using Enterprise_E_Commerce_Management_System.Application.Products.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Products.Results;
using Enterprise_E_Commerce_Management_System.ViewModels.UserProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Enterprise_E_Commerce_Management_System.Controllers
{
    [Authorize(Policy = "Products.Manage")]
    public class ProductController : Controller
    {
        private readonly IProductService _prdService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService prdService
            ,ICategoryService categoryService)
        {
            _prdService = prdService;
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _prdService.GetPageViewModel();
            return View(viewModel);
        }

        [HttpGet]

        public async Task<IActionResult> Search(ProductFilterDTO filter)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var listVM = await _prdService.GetAvailableListAsync(filter);
            return Json(listVM);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Product/CheckUniqueName")]
        public async Task<IActionResult> CheckUniqueCategoryIdNameAsync(
            string Name, int? CategoryId=null, int? Id=null)
        {
            bool isUnique = await _prdService.CheckUniqueCategoryIdNameAsync(Name, CategoryId, Id);
            return Json(isUnique);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateImage(IFormFile ImageFile)
        {
            return Json(_prdService.ValidateImage(ImageFile));
        }

        [HttpGet]
        public async Task<IActionResult> Form(int? Id)
        {
            if (Id.HasValue)//Edit
            {
                if (Id <= 0)
                    return BadRequest();

                storage.RefreshCookieMinutes(storage.ProductIdKey,Id.Value,HttpContext,30);
                var prdVM = await _prdService.GetFormViewModelAsync(Id.Value);
                if (prdVM == null)
                    return NotFound();

                return View("Edit",prdVM);
            }
            else//Add
            {
                var viewModel = await _prdService.GetFormViewModelAsync();
                return View("Create",viewModel);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Form(ProductFormViewModel viewModel) 
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = await 
                    _categoryService.GetSubListAsync();
                return viewModel.Id.HasValue? 
                    View("Edit",viewModel): View("Create",viewModel);
            }

            if (viewModel.Id.HasValue)//Edit
            {
              
                enUpdateProductResult result = await _prdService.UpdateProductAsync(viewModel);

                switch(result)
                {
                    case enUpdateProductResult.CategoryNotFound:
                        return NotFound("Category not found.");

                    case enUpdateProductResult.ProductNotFound:
                        return NotFound("Product not found.");

                    case enUpdateProductResult.NotUniqueImageName:
                        return NotFound("There is a product with same image name.");

                    case enUpdateProductResult.InvalidImage:
                        return NotFound("Invalid image.");

                    case enUpdateProductResult.NotUniqueName:
                        return NotFound("Not unique name.");

                    case enUpdateProductResult.NotUpdateMode:
                        return BadRequest("Invalid data.");
                    case enUpdateProductResult.Success:
                        {
                            this.SendTempMessage(enTempMessage.UpdatedSuccessfully);
                            break;
                        } 
                }  
            }
            else //Add
            {
                enCreateProductResult result = await _prdService.CreateProductAsync(viewModel);
                switch (result)
                {
                    case enCreateProductResult.CategoryNotFound:
                        return NotFound("Category not found."); 

                    case enCreateProductResult.NotUniqueImageName:
                        return NotFound("There is a product with same image name.");

                    case enCreateProductResult.InvalidImage:
                        return NotFound("Invalid image."); 

                    case enCreateProductResult.NotCreateMode:
                        return BadRequest("Invalid data.");

                    case enCreateProductResult.Success:
                        {
                            this.SendTempMessage(enTempMessage.CreateSucceeded);
                            break;
                        }
                } 
            }

            return RedirectToAction(nameof(Index));
        } 

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id <= 0)
                return BadRequest();

            enDeleteProductResult result = await _prdService
                .SoftOrHardDeleteProductAsync(Id);

            if (result==enDeleteProductResult.ProductNotFound)
                return NotFound("Product not found.");

            this.SendTempMessage(enTempMessage.DeletedSuccessfully);
            return RedirectToAction(nameof(Index));
        } 

    }
}
