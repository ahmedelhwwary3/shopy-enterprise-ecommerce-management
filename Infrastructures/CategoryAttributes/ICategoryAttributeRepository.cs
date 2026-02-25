using System.ComponentModel;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.CategoryAttributes
{
    public interface ICategoryAttributeRepository:IRepository<CategoryAttribute>
    {
        Task<List<enAttributeName>> GetAttributeNamesListByProductIdAsync(int productId);
    }
}
