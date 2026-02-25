using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Enterprise_E_Commerce_Management_System.Models.Carts;

namespace Enterprise_E_Commerce_Management_System.Application.CartItems
{
    public class CartItemService:ICartItemService
    {
        private readonly IUnitOfWork _uow; 
        public CartItemService(IUnitOfWork uow)
        {
            _uow = uow; 
        }
        public async Task<enCreateCartItemResult> CreateAsync(AddToCartDTO dto, int cartId, decimal unitPrice)
        {
            if(unitPrice<=0 ||dto.Quantity<=0)//Business Rule
                return enCreateCartItemResult.InvalidData;

            var item = new CartItem();
            item.CartId= cartId;
            item.UnitPrice= unitPrice;
            item.VariantId= dto.VariantId;
            item.Quantity= dto.Quantity;
            await _uow.CartItems.AddAsync(item);
            await _uow.SaveChangesAsync();
            return enCreateCartItemResult.Success;
        }
    }
}
