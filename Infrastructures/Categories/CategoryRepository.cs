using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Customers
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(CommerceDbContext context) : base(context) { } 

        public async Task<ICollection<CategoryNameIdItemDTO>> GetBaseListAsync()
        {
            return await _context.Categories
              .Where(ctg=>!ctg.ParentId.HasValue)
              .OrderBy(ctg => ctg.Name)
              .Select(c => new CategoryNameIdItemDTO() { Id = c.Id, Name = c.Name })
              .ToListAsync();
        }

        public async Task<ICollection<CategoryNameIdItemDTO>> GetSubListAsync()
        {
            return await _context.Categories
              .Where(ctg => ctg.ParentId.HasValue)
              .OrderBy(ctg=>ctg.Name)
              .Select(c => new CategoryNameIdItemDTO() { Id = c.Id, Name = c.Name })
              .ToListAsync();
        }
    }
}
