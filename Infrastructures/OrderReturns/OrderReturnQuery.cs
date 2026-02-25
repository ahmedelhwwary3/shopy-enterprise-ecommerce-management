using Dapper;
using Enterprise_E_Commerce_Management_System.Application.OrderReturns.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Orders;
using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.OrderReturns
{
    public class OrderReturnQuery : IOrderReturnQuery
    {
        private readonly IDbConnection _cn;
        public OrderReturnQuery(IDbConnection connection)
        {
            _cn = connection;   
        }

        public async Task<OrderReturnPagedListDTO> GetAllListAsync(OrderReturnFilterDTO filter, int currencyId)
        {
            var result = await _cn.QueryMultipleAsync(
                param:new { filter.ReturnStatus,filter.PageSize
                ,filter.Page,filter.CountryId,filter.Search,currencyId},
                sql: "sp_SearchOrderReturns",
                commandType:CommandType.StoredProcedure);

            var list = (await result.ReadAsync<OrderReturnItemDTO>()).ToList();
            int count = await result.ReadFirstOrDefaultAsync<int>();
            string code=await result.ReadFirstOrDefaultAsync<string>();
            return new OrderReturnPagedListDTO()
            {
                Count = count,
                Filter = filter,
                OrderReturns = list,
                CurrencyCode = code
            };
        }
    }
}
