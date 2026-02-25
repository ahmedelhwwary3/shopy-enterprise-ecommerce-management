using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Infrastructures.Customers;
using Enterprise_E_Commerce_Management_System.Infrastructures.Products;
using Enterprise_E_Commerce_Management_System.ViewModels.Category;
using System.Threading.Tasks;

namespace Enterprise_E_Commerce_Management_System.Application.Categories
{
    public class CategoryService: ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepo; 
        public CategoryService(IMapper mapper,
            ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            _mapper=mapper; 
        } 

        public async Task<List<CategoryNameIdItemViewModel>> GetBaseListAsync()
        {
            var list = await _categoryRepo.GetBaseListAsync();
            return _mapper.Map<List<CategoryNameIdItemViewModel>>(list);
        }
        public async Task<List<CategoryNameIdItemViewModel>> GetSubListAsync()
        {
            var list = await _categoryRepo.GetSubListAsync();
            return _mapper.Map<List<CategoryNameIdItemViewModel>>(list);
        }
       
    }
}
