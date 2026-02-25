using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.CategoryAttributes
{
    public class CategoryAttributeRepository:Repository<CategoryAttribute>, ICategoryAttributeRepository
    { 
        public CategoryAttributeRepository(CommerceDbContext context) : base(context) { }

        public async Task<List<enAttributeName>> GetAttributeNamesListByProductIdAsync(int productId)
        {
            int categoryId = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.CategoryId)
                .FirstOrDefaultAsync();

            return await _context.CategoryAttributes
                .Where(attr=>attr.CategoryId==categoryId)
                .Select(attr=>attr.Attribute.Name)
                .ToListAsync();
        }
    }
}
