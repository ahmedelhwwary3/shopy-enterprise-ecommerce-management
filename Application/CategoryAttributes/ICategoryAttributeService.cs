namespace Enterprise_E_Commerce_Management_System.Application.CategoryAttributes
{
    public interface ICategoryAttributeService
    {
        Task<List<enAttributeName>> GetAttributeNamesListByProductIdAsync(int productId);
    } 
}
