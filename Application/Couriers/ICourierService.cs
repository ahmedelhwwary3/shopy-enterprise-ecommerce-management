using Enterprise_E_Commerce_Management_System.Application.CartItems.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Couriers.Results;
using Enterprise_E_Commerce_Management_System.Models.Couriers;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.ViewModels.Courier;
using Microsoft.AspNetCore.Identity;

namespace Enterprise_E_Commerce_Management_System.Application.Couriers
{
    public interface ICourierService
    {
        Task<Int32> GetIdByUserIdAsync(string userId);
        Task<Int32> GetIdByUserNameAsync(string userName);
        Task AddAsync(Courier courier);
        Task<EditCouierViewModel> GetEditViewModelAsync(int courierId);
        Task<enUpdateCourierResult> UpdateAsync(EditCouierViewModel viewModel);
        Task<CourierPagedListViewModel> GetListAsync(CourierFilterViewModel filter);
        Task<Courier> GetByUserNameAsync(string userName);
        Task<CourierDetailsViewModel> GetDetailsViewModelByIdAsync(int courierId);
        Task<int> GetAssignedOrdersCountByCourierIdAsync(int courierId);
    }
}
