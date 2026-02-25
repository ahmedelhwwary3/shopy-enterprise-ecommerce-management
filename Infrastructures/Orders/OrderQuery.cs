using Dapper;
using Enterprise_E_Commerce_Management_System.Application.Orders;
using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Orders
{
    public class OrderQuery : IOrderQuery
    {
        private readonly IDbConnection _cn;
        public OrderQuery(IDbConnection connection)
        {
            _cn = connection;   
        }
 
        public async Task<int> CreateAndGetIdAsync(Order order)
        {
            return await _cn.ExecuteScalarAsync<int>(
                "sp_InsertOrder",
                param: order,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<OrderManagementPagedListDTO> GetUserAssignedListAsync(OrderManagementFilterDTO filter, int currencyId)
        {
            var result = await _cn.QueryMultipleAsync(
                param:new {filter.OrderStatus,filter.PaymentStatus,filter.Search,
                    filter.PageSize,filter.Page,filter.PaymentMethod,currencyId },
                sql: "sp_SearchUserAssignedOrders",
                commandType:CommandType.StoredProcedure);

            var list=(await result.ReadAsync<OrderManagementItemDTO>()).ToList();
            int count = await result.ReadFirstOrDefaultAsync<int>();
            string code=await result.ReadFirstOrDefaultAsync<string>();
            return new OrderManagementPagedListDTO()
            {
                Count=count,
                Filter=filter,
                Orders=list,
                CurrencyCode=code
            };
        }
    }
}
