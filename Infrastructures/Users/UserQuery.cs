using Dapper;
using Enterprise_E_Commerce_Management_System.Application.Users.DTOs;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Users
{
    public class UserQuery : IUserQuery
    {
        private readonly IDbConnection _cn;
        public UserQuery(IDbConnection connection)
        {
            _cn = connection;
        }
          
        public async Task<UserPagedListDTO> GetListDtoAsync(UserFilterDTO filter)
        {

            var result= await _cn.QueryMultipleAsync(
               sql: "sp_SearchUsers",
               param: filter,
               commandType: CommandType.StoredProcedure); 

            var list = (await result.ReadAsync<UserItemDTO>()).ToList();
            int count = await result.ReadFirstOrDefaultAsync<int>();
            return new UserPagedListDTO()
            {
                Count=count,
                Users=list
            };
        }
    }
}
