using Dapper; 
using Enterprise_E_Commerce_Management_System.Models.Carts;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Carts
{
    public class CartQuery : ICartQuery
    {
        private readonly IDbConnection _cn;
        public CartQuery(IDbConnection connection)
        {
            _cn = connection;
        }
         
        public async Task<int> CreateAndGetId(Cart cart)
        {
            int cartId = await _cn.ExecuteScalarAsync<int>(
                sql: "sp_InsertCart",
                param: new { cart.CustomerId,cart.CreateDate,cart.IsActive},
                commandType: CommandType.StoredProcedure); 

            return cartId;
        }
    }
}
