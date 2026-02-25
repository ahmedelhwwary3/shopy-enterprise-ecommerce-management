using Enterprise_E_Commerce_Management_System.Application.Attributes.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs; 
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.CartItems
{
    public class CartItemRepository:Repository<CartItem>,ICartItemRepository
    {
        public CartItemRepository(CommerceDbContext context):base(context) { }
        public async Task<CartDetailsDTO> GetDetailsDtoByCartIdAsync(int cartId, int currencyId)
        {
            var currency = await _context.Currencies
                .Where(curr => curr.Id == currencyId)
                .Select(curr => new { curr.Code ,curr.DollarRate})
                .FirstOrDefaultAsync(); 

            var itemList = await (from item in _context.CartItems
                                  where item.CartId == cartId
                                  select new CartItemDTO()
                                  {
                                      Id = item.Id,
                                      UnitPrice = item.UnitPrice * currency.DollarRate,
                                      Quantity = item.Quantity,
                                      TotalPrice = (decimal)item.Quantity * item.UnitPrice * currency.DollarRate,
                                      ImageName = item.Variant.Product.ImageName,
                                      ProductName = item.Variant.Product.Name,
                                      Attributes = item.Variant.AttributeValues
                                      .Select(attr=>new VariantAttributeNameValueItemDTO()
                                      {
                                          Value= attr.Value,Name=attr.Attribute.Name
                                      }).ToList() 
                                  }).ToListAsync();
            var dto = new CartDetailsDTO()
            {
                Items = itemList,
                TotalCount= itemList.Sum(i=>i.Quantity),
                TotalPrice = itemList.Sum(i=>i.TotalPrice),
                Id =cartId,
                Code= currency.Code
            };
            return dto;
        }
        public async Task<int> GetItemsTotalCountAsync(int Id)
        {
            int count= await (from i in _context.CartItems
                              where i.CartId == Id
                              select i.Quantity)
                              .SumAsync();
            return count;
        }
        public async Task<decimal> GetItemsTotalPriceAsync(int Id)
        {
            return await _context.CartItems
                    .Where(i => i.CartId == Id) 
                    .SumAsync(i => i.Quantity * i.UnitPrice);
        }
    }
}
