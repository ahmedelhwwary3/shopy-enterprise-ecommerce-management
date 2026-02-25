

using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;
using Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues;

namespace Enterprise_E_Commerce_Management_System.Application.AttributeValues
{
    public interface IAttributeValueService
    { 
        Task DeleteValuesByVariantIdAsync(int variantId);
    }
}
