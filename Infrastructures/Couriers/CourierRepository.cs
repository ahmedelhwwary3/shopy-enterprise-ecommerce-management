using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs;
using Enterprise_E_Commerce_Management_System.Migrations;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Couriers;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Couriers
{
    public class CourierRepository : Repository<Courier>, ICourierRepository
    {
        public CourierRepository(CommerceDbContext context) : base(context) { }

        public async Task<int> GetIdByUserIdAsync(string userId)
        { 
            return await _context.Couriers
                .Where(c => c.UserId == userId)
                .Select(c => c.Id).FirstOrDefaultAsync(); 
        }

        public async Task<int> GetIdByUserNameAsync(string userName)
        {
            return await _context.Couriers
                 .Where(c => c.User.UserName == userName)
                 .Select(c => c.Id).FirstOrDefaultAsync();
        }

        public async Task<(int, int)> GetCountryIdAndProviderIdAsync(int courierId)
        {
            var data = await _context.Couriers
           .Where(c => c.Id == courierId)
           .Select(c => new
           {
               CountryId = c.User.CountryId,
               ShippingProviderId = c.ShippingProviderId
           })
           .FirstOrDefaultAsync();
            return new(data.CountryId,data.ShippingProviderId);
        }

        public async Task<Courier> GetByUserNameAsync(string userName)
        {
            return await _context.Couriers
                .Where(c=>c.User.UserName==userName)
                .FirstOrDefaultAsync();
        }

        public async Task<CourierDetailsDTO> GetDetailsDtoByIdAsync(int courierId)
        {
            return await _context.Couriers.Where(c => c.Id == courierId)
                .Select(c => new CourierDetailsDTO()
                {
                    Country = c.User.Country.Name,
                    Email = c.User.Email,
                    PhoneNumber = c.User.PhoneNumber,
                    ShippingProvider = c.ShippingProvider.Name,
                    UserName = c.User.UserName,
                    Id = c.Id,
                    IsActive=c.IsActive
                }).FirstOrDefaultAsync();
        }

        public async Task<int> GetAssignedOrdersCountByCourierIdAsync(int courierId)
        {
            return await _context.Shipments
                .Where(sh => sh.CourierId == courierId &&
                sh.ShipmentStatus == enShippingStatus.AssignedForCourier 
                && sh.OrderStatus!=enOrderStatus.Cancelled)
                .CountAsync();
        }
    }
}
