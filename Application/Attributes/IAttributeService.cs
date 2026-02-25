

using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.CartItems.Results;

namespace Enterprise_E_Commerce_Management_System.Application.Attributes
{
    public interface IAttributeService
    {
        Task<int> GetIdByNameAsync(enAttributeName name);
    }
}
