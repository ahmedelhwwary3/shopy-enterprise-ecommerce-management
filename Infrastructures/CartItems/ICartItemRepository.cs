using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs;
using Enterprise_E_Commerce_Management_System.Models.CartItems;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.CartItems
{
    public interface ICartItemRepository:IRepository<CartItem>
    {
        Task<CartDetailsDTO> GetDetailsDtoByCartIdAsync(int cartId, int currencyId);
        Task<int> GetItemsTotalCountAsync(int Id);
        Task<decimal> GetItemsTotalPriceAsync(int Id);
    }
}
