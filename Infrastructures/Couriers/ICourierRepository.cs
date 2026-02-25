using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Couriers;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.ViewModels.Courier;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Couriers
{
    public interface ICourierRepository : IRepository<Courier>
    {
        Task<CourierDetailsDTO> GetDetailsDtoByIdAsync(int courierId);
        Task<int> GetIdByUserIdAsync(string userId);
        Task<int> GetIdByUserNameAsync(string userName);
        Task<(int, int)> GetCountryIdAndProviderIdAsync(int courierId);
        Task<Courier> GetByUserNameAsync(string userName);
        Task<int> GetAssignedOrdersCountByCourierIdAsync(int courierId);
    }
}
