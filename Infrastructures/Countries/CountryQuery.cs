using Dapper;
using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Countries
{
    public class CountryQuery : ICountryQuery
    {
        private readonly IDbConnection _cn;
        public CountryQuery(IDbConnection connection)
        {
            _cn = connection;   
        }
        public async Task<ICollection<CountryNameIdDTO>> GetAllDtoAsync()
        {
            var list = await _cn.QueryAsync<CountryNameIdDTO>(
                "sp_GetAllCountriesNameId",
                commandType: CommandType.StoredProcedure);
            return list.ToList();
        }
    }
}
