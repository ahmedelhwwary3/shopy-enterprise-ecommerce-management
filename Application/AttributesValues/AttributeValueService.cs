using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues;

namespace Enterprise_E_Commerce_Management_System.Application.AttributeValues
{
    public class AttributeValueService : IAttributeValueService
    {
        private readonly IUnitOfWork _uow; 
        public AttributeValueService(IUnitOfWork uow)
        {
            _uow = uow; 
        }
          
        public async Task DeleteValuesByVariantIdAsync(int variantId)
        {
            var value= await _uow.AttributeValues.GetListByVariantIdAsync(variantId);
            foreach (var item in value)
            {
                _uow.AttributeValues.DeleteByVariantIdValueIdKeyAsync(item.VariantId, item.AttributeId);
            }
        }
    }
}
