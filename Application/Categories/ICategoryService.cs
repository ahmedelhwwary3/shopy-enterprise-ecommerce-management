using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Category;

namespace Enterprise_E_Commerce_Management_System.Application.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryNameIdItemViewModel>> GetBaseListAsync();
        Task<List<CategoryNameIdItemViewModel>> GetSubListAsync();
    }
}
