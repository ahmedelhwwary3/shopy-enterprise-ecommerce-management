using Enterprise_E_Commerce_Management_System.Infrastructures;

namespace Enterprise_E_Commerce_Management_System.Application.CategoryAttributes
{
    public class CategoryAttributeService:ICategoryAttributeService
    {
        private readonly IUnitOfWork _uow;
        public CategoryAttributeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<enAttributeName>> GetAttributeNamesListByProductIdAsync(int productId)
        {
            return await _uow.CategoryAttributes.GetAttributeNamesListByProductIdAsync(productId);
        }
    }
}
