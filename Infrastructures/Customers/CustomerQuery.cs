using Dapper;
using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Customers
{
    public class CustomerQuery : ICustomerQuery
    {
        private readonly IDbConnection _cn;
        public CustomerQuery(IDbConnection connection)
        {
            _cn = connection;
        }
        public async Task<CustomerPagedListDTO> GetListAsync(CustomerFilterDTO filter)
        {
           var result = await _cn.QueryMultipleAsync(
                sql: "sp_SearchCustomers", 
                param:filter,
                commandType: CommandType.StoredProcedure);
            var customerList = (await result.ReadAsync<CustomerItemDTO>()).ToList();
            int count = await result.ReadFirstOrDefaultAsync<int>();
            var countryList= (await result.ReadAsync<CountryNameIdViewModel>()).ToList();
            return new CustomerPagedListDTO()
            {   
                Count = count,
                Customers = customerList ,
                Countries=countryList
            };
        }
        public async Task<CustomerDetailsDTO> GetDetailsDtoByIdAsync(int Id)
        {
            var result = await _cn.QueryMultipleAsync(
                sql: "sp_GetCustomerDetailsById",
                param: Id,
                commandType: CommandType.StoredProcedure);
            var fullName = await result.ReadFirstOrDefaultAsync<FullNameDTO>();
            var address = await result.ReadFirstOrDefaultAsync<AddressDTO>();
            var details = await result.ReadFirstOrDefaultAsync<CustomerDetailsDbRow>();
            var countryList = (await result.ReadAsync<CountryNameIdDTO>()).ToList();
            return new CustomerDetailsDTO()
            {
                Address= address, 
                FullName= fullName,
                IsActive= details.IsActive,
                CreatedDate= details.CreatedDate,
                Email= details.Email,
                Id= details.Id,
                PhoneNumber= details.PhoneNumber
            };
        }
    }
}
