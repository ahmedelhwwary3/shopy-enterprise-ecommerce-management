using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs; 
using attr= Enterprise_E_Commerce_Management_System.Models.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Attributes
{
    public class AttributeRepository : Repository<attr.Attribute>,IAttributeRepository
    {
        public AttributeRepository(CommerceDbContext context):base(context) { }

        public async Task<int> GetIdByNameAsync(enAttributeName name)
        {
            return await _context.Attributes
                .Where(attr => attr.Name == name)
                .Select(attr => attr.Id)
                .FirstOrDefaultAsync();
        }
    }
}
