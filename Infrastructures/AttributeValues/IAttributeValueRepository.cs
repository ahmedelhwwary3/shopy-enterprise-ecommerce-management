using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs;
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues;
using attr= Enterprise_E_Commerce_Management_System.Models.Attributes;
namespace Enterprise_E_Commerce_Management_System.Infrastructures.AttributeValues
{
    public interface IAttributeValueRepository : IRepository<AttributeValue>
    {
        Task<List<AttributeValue>> GetListByVariantIdAsync(int variantId);
        Task DeleteByVariantIdValueIdKeyAsync(int VariantId, int AttributeId);
    }
}
