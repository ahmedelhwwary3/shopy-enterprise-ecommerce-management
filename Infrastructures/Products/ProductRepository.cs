using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CommerceDbContext context) : base(context) { }
        
        public async Task<int> GetAllVariantsStockQuantityByIdAsync(int Id)
        {
            return await _context.Variants
                .Where(v => v.ProductId == Id)
                .SumAsync(v => v.StockQuantity);
        }
        public async Task<bool> HasVariants(int Id)
        {
            return await _context.Variants
                .AnyAsync(item => item.ProductId == Id);
        }
        public async Task<bool> ExistsByNameAndCategoryId(string Name, int CategoryId)
        {
            return await _context.Products.AnyAsync(c => c.Name==Name && c.CategoryId==CategoryId);
        }

        public async Task<int> GetCategoryIdByIdAsync(int Id)
        {
            return await _context.Products
                .Where(p => p.Id == Id)
                .Select(p => p.CategoryId)
                .FirstOrDefaultAsync();
        }

        async Task<bool> IProductRepository.ExistsByImageNameAsync(string ImageName)
        {
            return await _context.Products.AnyAsync(p => p.ImageName == ImageName);
        }

        public async Task<bool> HasVariantAttributeAsync(int ProductId, enAttributeName Name, string Value)
        {
            return await _context.AttributeValues
                .AnyAsync(att => att.Variant.ProductId == ProductId &&
                att.Attribute.Name == Name && att.Value == Value); 
        }
         
    }
}
