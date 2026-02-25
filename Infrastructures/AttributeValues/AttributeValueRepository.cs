using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs; 
using attr= Enterprise_E_Commerce_Management_System.Models.Attributes;
using Microsoft.EntityFrameworkCore;
using Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.AttributeValues
{
    public class AttributeValueRepository : Repository<AttributeValue>,IAttributeValueRepository
    {
        public AttributeValueRepository(CommerceDbContext context):base(context) { }

        public async Task<List<AttributeValue>> GetListByVariantIdAsync(int variantId)
        {
            return await _context.AttributeValues
                .Where(attrVal =>attrVal.VariantId==variantId)
                .ToListAsync();
        }

        public async Task DeleteByVariantIdValueIdKeyAsync(int VariantId, int AttributeId)
        {
            var attributeValue = await _context.AttributeValues
                .FirstOrDefaultAsync(
                attrVal=>attrVal.AttributeId== AttributeId && 
                attrVal.VariantId==VariantId);

            _context.AttributeValues.Remove(attributeValue);
        }
    }
}
