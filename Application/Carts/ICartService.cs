using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.ViewModels.Cart;

namespace Enterprise_E_Commerce_Management_System.Application.Carts
{
    public interface ICartService
    {
        Task<int> CreateAndGetIdAsync(int? customerId);
        Task<CartDetailsViewModel> GetDetailsViewModelByIdAsync(int Id,int currencyId);
        Task<int> GetItemsTotalCountByIdAsync(int Id);
        Task<decimal> GetItemsTotalPriceByIdAsync(int Id);
        void Update(Cart cart);
        Task<Cart> GetByIdAsync(int Id);
        Task<Cart> GetWithItemsByIdAsync(int Id);
        Task DeleteItemByItemIdAsync(int CartId, int ItemId);
        Task DeleteByIdAsync(int cartId);
    }
}
