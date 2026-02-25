using Dapper;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Shipments
{
    public class ShipementQuery : IShipementQuery
    {
        private readonly IDbConnection _cn;
        public ShipementQuery(IDbConnection connection)
        {
            _cn = connection;   
        }

        public async Task<AssignedShipmentPagedListDTO> 
            GetCourierAssignedOrderListViewModelAsync(AssignedShipmentFilterDTO filter, int currencyId)
        {
            var result = await _cn.QueryMultipleAsync(
                sql: "sp_SearchCourierAssignedOrders",
                param: new { filter.CourierId, filter.PageSize, filter.Page, filter.Search, filter.ShipmentStatus, currencyId },
                commandType: CommandType.StoredProcedure);

            var list = (await result.ReadAsync<AssignedShipmentItemDTO>()).ToList();
            int count = await result.ReadFirstOrDefaultAsync<int>();
            string currencyCode = await result.ReadFirstOrDefaultAsync<string>();
            return new AssignedShipmentPagedListDTO()
            {
                Count= count,
                Filter= filter,
                Orders= list,
                CurrencyCode= currencyCode
            };

        }

        public async Task<AvailableOrdersPagedListDTO> GetAvailableOrdersForCourierAsync(AvailableOrderFilterDTO filter, int currencyId)
        {
            var result = await _cn.QueryMultipleAsync(
                "sp_SearchCourierAvailableOrdersForAssignment",
                param: new { filter.Search,filter.Page,filter.PageSize,filter.CountryId, currencyId },
                commandType: CommandType.StoredProcedure);
            var list = (await result.ReadAsync<AvailableOrderItemDTO>()).ToList();
            int count = await result.ReadFirstOrDefaultAsync<int>();
            string currencyCode = await result.ReadFirstOrDefaultAsync<string>();
            return new AvailableOrdersPagedListDTO()
            {
                Count = count,
                Orders = list,
                Filter = filter,
                CurrencyCode= currencyCode

            };
        }
    }
}
