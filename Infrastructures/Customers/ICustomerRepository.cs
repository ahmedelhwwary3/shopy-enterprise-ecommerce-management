using Enterprise_E_Commerce_Management_System.Models.Customers;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Customers
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByEmailOrPhone (string email, string phoneNumber);
        Task<Customer> GetCustomerById(int Id);
        Task<int?> GetIdByEmailOrPhone(string Email,string PhoneNumber);
    }
}
