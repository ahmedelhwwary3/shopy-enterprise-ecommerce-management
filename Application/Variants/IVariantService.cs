using Enterprise_E_Commerce_Management_System.Application.Variants.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Variants.Results;
using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using Enterprise_E_Commerce_Management_System.ViewModels.Variant;

namespace Enterprise_E_Commerce_Management_System.Application.Variants
{
    public interface IVariantService
    {
        Task<enCreateVariantResult> CreateAsync(VariantFormViewModel viewModel,int currencyId);
        Task<enUpdateVariantResult> UpdateAsync(VariantFormViewModel viewModel,int currencyId);
        Task<enDeleteVariantResult> SoftOrHardDeleteAsync(int Id);
        Task<VariantListViewModel> GetListByProductIdAsync(VariantFilterDTO filter,int currencyId);
        Task<VariantFormViewModel> GetVariantFormViewModelAsync( int productId,int currencyId, int? variantId = null);
        Task<bool> CheckSkuUniqueAsync(string sku, int? variantId = null);
        //Task<bool> CheckUniqueColorSizeAsync(enVariantSize size, enVariantColor color, int variantId = 0);
        Task<decimal> GetUnitPriceAsync(int Id);
        Task<enRecalculateVariantStatusResult> RecalculateQuantityAndStatusAsync(int variantId, int quantityToAdd);
        Task<bool> CheckUniqueVariantAttributeByProductIdAsync(enAttributeName Name, string Value,int ProductId,int? VariantId=null);
    }
}
