using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs;
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using attr= Enterprise_E_Commerce_Management_System.Models.Attributes;
namespace Enterprise_E_Commerce_Management_System.Infrastructures.Attributes
{
    public interface IAttributeRepository : IRepository<attr.Attribute>
    {
        Task<int> GetIdByNameAsync(enAttributeName name);
    }
}
