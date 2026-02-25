

using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;

namespace Enterprise_E_Commerce_Management_System.Application.CartItems
{
    public interface ICartItemService
    {
        Task<enCreateCartItemResult> CreateAsync(AddToCartDTO dto,int cartId,decimal unitPrice);
    }
}
