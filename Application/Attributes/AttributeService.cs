using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Enterprise_E_Commerce_Management_System.Models.Carts;

namespace Enterprise_E_Commerce_Management_System.Application.Attributes
{
    public class AttributeService : IAttributeService
    {
        private readonly IUnitOfWork _uow; 
        public AttributeService(IUnitOfWork uow)
        {
            _uow = uow; 
        }

        public async Task<int> GetIdByNameAsync(enAttributeName name)
        {
            return await _uow.Attributes.GetIdByNameAsync(name);
        }
    }
}
