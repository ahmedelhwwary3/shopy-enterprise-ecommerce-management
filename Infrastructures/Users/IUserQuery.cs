using Enterprise_E_Commerce_Management_System.Application.Users.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.ViewModels.User;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Users 
{
    public interface IUserQuery
    {
        Task<UserPagedListDTO> GetListDtoAsync(UserFilterDTO filter);
    }
}
