using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.Models.Variants;
namespace Enterprise_E_Commerce_Management_System.Infrastructures.Products
{
    public interface IProductRepository:IRepository<Product>
    {
        
        Task<int> GetAllVariantsStockQuantityByIdAsync(int Id);
        Task<bool> HasVariants(int Id);

        Task<bool> ExistsByNameAndCategoryId(string Name, int CategoryId);
        Task<int> GetCategoryIdByIdAsync(int Id);
        Task<bool> ExistsByImageNameAsync(string ImageName);
        Task<bool> HasVariantAttributeAsync(int ProductId, enAttributeName Name,string Value); 

    }
}
