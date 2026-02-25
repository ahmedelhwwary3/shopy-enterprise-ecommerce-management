using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts;
namespace Enterprise_E_Commerce_Management_System.Application.ShoppingProducts
{
    public interface IShoppingService
    {
        Task<ShoppingPageViewModel> GetPageViewModelAsync(int CurrencyId);
        Task<ShoppingPagedListViewModel> GetProductListAsync(ShoppingFilterDTO filter);
        Task<List<ProductPriceItemViewModel>> GetProductPriceRangeList(int CurrencyId);
        Task<ShoppingtDetailsViewModel>GetShoppingtDetailsByProductIdAsync(int ProductId,int currencyId); 
    }
}
