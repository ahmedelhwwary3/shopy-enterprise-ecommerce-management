using Enterprise_E_Commerce_Management_System.Models.Customers;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Customers
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CommerceDbContext context) : base(context) { }

        public async Task<Customer> GetByEmailOrPhone(string email, string phoneNumber)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email || c.PhoneNumber == phoneNumber);
        }
        public async Task<Customer> GetCustomerById(int Id)
        {
            return await _context.Customers
                .AsTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.Id==Id);
        }
        public async Task<int?> GetIdByEmailOrPhone(string Email, string PhoneNumber)
        {
            return await _context.Customers
                .Where(c =>
                 (!string.IsNullOrEmpty(Email) && c.Email == Email) ||
                 (!string.IsNullOrEmpty(PhoneNumber) && c.PhoneNumber == PhoneNumber))
                .Select(c=>c.Id)
                .FirstOrDefaultAsync();
        }
    }
}
