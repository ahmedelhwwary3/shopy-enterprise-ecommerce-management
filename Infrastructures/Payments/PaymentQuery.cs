using Dapper;
using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Payments.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Payments
{
    public class PaymentQuery : IPaymentQuery
    {
        private readonly IDbConnection _cn;
        public PaymentQuery(IDbConnection connection)
        {
            _cn = connection;
        }

        public async Task<PaymentPagedListDTO> GetAllListAsync(PaymentFilterDTO filter, int currencyId)
        {
            var result = await _cn.QueryMultipleAsync(
                sql: "sp_SearchPayments",
                param: new { filter.PageSize,filter.Page,filter.PaymentStatus,
                    filter.Search,filter.CountryId,filter.PaymentMethod,currencyId},
                commandType: CommandType.StoredProcedure);
            var paymentList = (await result.ReadAsync<PaymentItemDTO>()).ToList();
            int count = await result.ReadFirstOrDefaultAsync<int>();
            string code=await result.ReadFirstOrDefaultAsync<string>();
            return new PaymentPagedListDTO()
            {
                Count = count,
                Payments = paymentList,
                Filter = filter,
                CurrencyCode=code
            };
        }

        
      
    }
}
