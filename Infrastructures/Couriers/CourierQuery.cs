using Dapper;
using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Countries
{
    public class CourierQuery : ICourierQuery
    {
        private readonly IDbConnection _cn;
        public CourierQuery(IDbConnection connection)
        {
            _cn = connection;   
        } 

        public async Task<CourierPagedListDTO> GetListAsync(CourierFilterDTO filter)
        {
            var result = await _cn.QueryMultipleAsync(
                 "sp_SearchCouriers",
                 param:filter,
                 commandType: CommandType.StoredProcedure);
            var list = (await result.ReadAsync<CourierItemDTO>()).ToList();
            int count = await result.ReadFirstOrDefaultAsync<int>();
            return new CourierPagedListDTO()
            {
                Count=count,
                Couriers=list,
                Filter=filter
            };
        }
    }
}
