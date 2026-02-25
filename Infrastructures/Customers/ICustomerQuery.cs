using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Customers
{
    public interface ICustomerQuery
    {
        Task<CustomerPagedListDTO> GetListAsync(CustomerFilterDTO filter);
        Task<CustomerDetailsDTO> GetDetailsDtoByIdAsync(int Id);
    }
}
