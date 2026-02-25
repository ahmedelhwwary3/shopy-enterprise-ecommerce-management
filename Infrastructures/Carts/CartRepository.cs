using Enterprise_E_Commerce_Management_System.Models.Carts;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Carts
{
    public class CartRepository :Repository<Cart> ,ICartRepository
    {
        public CartRepository(CommerceDbContext context) : base(context) { }

        public async Task DeleteItemByItemIdAsync(int CartId, int ItemId)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(i => i.CartId == CartId && i.Id == ItemId);
            _context.CartItems.Remove(item);
        }

        public async Task<bool> IsEmpty(int CartId)
        {
            return ! await _context.CartItems
                .AnyAsync(c => c.CartId == CartId);
        }
    }
}
