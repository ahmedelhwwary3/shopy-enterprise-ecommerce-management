using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Products.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs; 

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Products
{
    public interface IProductQuery
    {
        Task<ProductPagedListDTO> GetProductListWithCountAsync(ProductFilterDTO filter);
        Task<ShoppingPagedListDTO> GetShoppingListAsync(ShoppingFilterDTO filter);
        Task<ShoppingtDetailsDTO> GetShoppingtDetailsViewModelByIdAsync(int ProductId, int currencyId);
    }
}
