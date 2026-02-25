using Enterprise_E_Commerce_Management_System.Application.Attributes.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Variants.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Variants;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using Enterprise_E_Commerce_Management_System.ViewModels.Variant;
namespace Enterprise_E_Commerce_Management_System.Infrastructures.Products
{
    public interface IVariantRepository:IRepository<Variant>
    {
        Task<bool> ExistsBySKUAsync(string sku);
        Task<bool> HasOrders(int Id);
        Task<VariantListDTO> GetListByProductIdAsync(VariantFilterDTO filter,int currencyId); 
        Task<decimal> GetUnitPriceAsync(int Id);
        Task<List<VariantAttributeNameValueItemDTO>> GetAttributesNameValueListByIdAsync(int Id);
        Task<Variant> GetAsReadOnlyIncludeAttributesByIdAsync(int Id);
        Task<Variant> GetIncludeAttributesByIdAsync(int Id);
    }
}
