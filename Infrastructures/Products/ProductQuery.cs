using Dapper;
using Enterprise_E_Commerce_Management_System.Application.Attributes.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Products.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts;
using System.Data;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Products
{
    public class ProductQuery : IProductQuery
    {
        private readonly IDbConnection _connection;
        public ProductQuery(IDbConnection connection)
        {
            _connection=connection;
        }
        public async Task<ProductPagedListDTO> GetProductListWithCountAsync(ProductFilterDTO filter)
        {
           var result= await _connection.QueryMultipleAsync(
               "sp_SearchUserProducts",
               filter,
               commandType:CommandType.StoredProcedure);
           var list = (await result.ReadAsync<ProductItemDTO>()).ToList();
           int count = await result.ReadFirstOrDefaultAsync<int>();
            return new ProductPagedListDTO()
            {
                Count= count,
                Products = list
            };
        }
        public async Task<ShoppingPagedListDTO>
            GetShoppingListAsync(ShoppingFilterDTO filter)
        {
            var result =await _connection.QueryMultipleAsync(
                "sp_SearchShoppingProducts",
                filter,
                commandType:CommandType.StoredProcedure);
            string code= await result.ReadFirstOrDefaultAsync<string>();
            var list = (await result.ReadAsync<ShoppingItemDTO>()).ToList();
            int count= await result.ReadFirstOrDefaultAsync<int>();
            return new ShoppingPagedListDTO()
            {
                Count = count,
                Products = list,
                Code=code
            }; 
        }

        public async Task<ShoppingtDetailsDTO>
         GetShoppingtDetailsViewModelByIdAsync(int ProductId, int currencyId)
        {
            var result = await _connection.QueryMultipleAsync(
                "sp_GetShoppingDetailsByProductId",
                param: new { ProductId=ProductId, CurrencyId= currencyId },
                commandType: CommandType.StoredProcedure);
            string code = await result.ReadFirstOrDefaultAsync<string>();
            var dto = await result.ReadFirstOrDefaultAsync<ShoppingtDetailsDTO>();
            dto.Code = code;
            dto.Variants = (await result.ReadAsync<ShoppingVariantItemDTO>()).ToList(); 
            var attributes= (await result.ReadAsync<VariantAttributeShoppingItemDTO>()).ToList();
            foreach(var v in dto.Variants)
            {
                v.Attributes = attributes
                    .Where(attr => attr.VariantId == v.Id)
                    .ToList();
            }
            return dto;
        }
    }
}
