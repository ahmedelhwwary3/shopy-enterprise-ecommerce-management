
using Enterprise_E_Commerce_Management_System.Application.Products.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Products.Results;
using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.Models.Variants;
using Enterprise_E_Commerce_Management_System.ViewModels.UserProducts;
namespace Enterprise_E_Commerce_Management_System.Application.Products
{
    public interface IProductService
    {
        bool ValidateImage(IFormFile image); 
        Task<enCreateProductResult> CreateProductAsync(ProductFormViewModel dto); 
        Task<enUpdateProductResult> UpdateProductAsync(ProductFormViewModel dto);
        Task<enDeleteProductResult> SoftOrHardDeleteProductAsync(int Id);
        Task<ProductFormViewModel> GetFormViewModelAsync(int? Id=null);
        Task<ProductPagedListViewModel> GetAvailableListAsync(ProductFilterDTO filter);

        /// <summary>
        /// Automatically recalculates product status based on variants stock.
        /// Sets product to Active if any variant has quantity > 0,
        /// otherwise sets it to Inactive.
        /// This is a system-driven business rule, not a user action.
        /// <param name="Id"></param>
        /// <returns></returns>
        Task RecalculateProductStatusAsync(int Id);
        /// <summary>
        /// Manually changes product status by an admin/user decision,
        /// regardless of current variants quantities.
        /// Used to temporarily enable or disable a product.
        /// <param name="Id"></param>
        /// <returns></returns> 
        Task<ProductPageViewModel> GetPageViewModel();
        Task<bool> CheckImageUniqueNameAsync(string ImageName, int? Id = null);
        Task<bool> CheckUniqueCategoryIdNameAsync(string Name, int? CategoryId=null, int? Id=null);
        Task<Product> GetAsReadOnlyByIdAsync(int Id);
        Task<bool> HasVariantAttributeAsync(int ProductId, enAttributeName Name,string Value); 
    }
}
