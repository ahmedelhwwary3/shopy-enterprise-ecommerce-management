using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Variants.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Variants;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using Enterprise_E_Commerce_Management_System.ViewModels.Variant;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Enterprise_E_Commerce_Management_System.Application.Attributes.DTOs;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Products
{
    public class VariantRepository : Repository<Variant>, IVariantRepository
    {
        public VariantRepository(CommerceDbContext context) : base(context) 
        {

        }
        public async Task<decimal> GetUnitPriceAsync(int Id)
        { 
            return await _context.Variants
                .Where(v=>v.Id==Id)
                .Select(v => v.Price)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> ExistsBySKUAsync(string sku)
        {
            return await _context.Variants.AnyAsync(v=>v.SKU==sku);
        }

        public async Task<bool> HasOrders(int Id)
        {
            return await _context.OrderItems
                .AnyAsync(item => item.Variant.Id == Id);
        }
        public async Task<VariantListDTO> GetListByProductIdAsync(VariantFilterDTO filter,int currencyId)
        {
            var page = filter.Page < 1 ? 1 : filter.Page;
            var pageSize = filter.PageSize < 1 ? 10 : filter.PageSize;

            var currency = await _context.Currencies
                .Where(curr => curr.Id == currencyId) 
                .Select(curr=>new {curr.DollarRate,curr.Code})
                .FirstOrDefaultAsync();

            int count = await _context.Variants
                .Where(v =>
                 v.ProductId == filter.ProductId &&
                 (!filter.IsActive.HasValue || v.IsActive == filter.IsActive) &&
                 (string.IsNullOrEmpty(filter.SKU) || v.SKU.Contains(filter.SKU.Trim())))
                .CountAsync();

            var dto= await _context.Variants
                .Where(v =>
                 v.ProductId == filter.ProductId &&
                 (!filter.IsActive.HasValue||v.IsActive ==filter.IsActive)&&
                 (string.IsNullOrEmpty(filter.SKU) || v.SKU.Contains(filter.SKU.Trim())))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(v=> new VariantItemDTO() 
                { 
                    Id =v.Id, 
                    Price = v.Price * currency.DollarRate, 
                    Cost = v.Cost * currency.DollarRate, 
                    SKU =v.SKU,
                    StockQuantity=v.StockQuantity,
                    IsActive=v.IsActive 
                })
                .ToListAsync();
            return new VariantListDTO()
            {
                Variants= dto,
                Count= count,
                CurrencyCode= currency.Code
            };
        }

        public async Task<List<VariantAttributeNameValueItemDTO>> GetAttributesNameValueListByIdAsync(int Id)
        {
            return await _context.AttributeValues
                .Where(attr => attr.VariantId == Id)
                .Select(attr => new VariantAttributeNameValueItemDTO()
                {
                    Name = attr.Attribute.Name,
                    Value = attr.Value
                }).ToListAsync();
        }

        public async Task<Variant> GetAsReadOnlyIncludeAttributesByIdAsync(int Id)
        {
            return await _context.Variants
                .AsNoTracking()
                .Include(v => v.AttributeValues)
                .ThenInclude(attr => attr.Attribute)
                .FirstOrDefaultAsync(v=>v.Id==Id);
        }

        public async Task<Variant> GetIncludeAttributesByIdAsync(int Id)
        {
            return await _context.Variants 
                .Include(v => v.AttributeValues)
                .ThenInclude(attr => attr.Attribute)
                .FirstOrDefaultAsync(v => v.Id == Id);
        }
    }
}
