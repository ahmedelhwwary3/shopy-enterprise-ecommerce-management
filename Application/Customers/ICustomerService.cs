using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Customers.Results;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;

namespace Enterprise_E_Commerce_Management_System.Application.Customers
{
    public interface ICustomerService
    {
        Task<enUpdateCustomerResult> UpdateAsync(CustomerFormViewModel customer);
        Task<enCreateCustomerResult> CreateIfNotExistsAsync(CustomerFormViewModel customer);
        Task<CustomerDetailsWithCountryListViewModel> GetDetailsByIdAsync(int customerId);
        Task<CustomerPagedListViewModel> GetListAsync(CustomerFilterViewModel filter);
        Task<CustomerFormViewModel> GetFormViewModelAsync(int? customerId=null);
        Task<enChangeCustomerStatusResult> ChangeCustomerStatusByIdAsync(int customerId);
        Task<int?> GetIdByEmailOrPhoneAsync(string Email,string PhoneNumber);
        Task<Customer> GetAsReadOnlyByIdAsync(int customerId);
    }
}
