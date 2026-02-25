using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Carts;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Carts
{
    public interface ICartQuery
    {
        Task<int> CreateAndGetId(Cart cart);
    }
}
