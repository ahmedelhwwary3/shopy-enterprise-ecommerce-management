using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.ViewModels.Category;

namespace Enterprise_E_Commerce_Management_System.Application.Categories.Mapping
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryNameIdItemDTO>();
            CreateMap<CategoryNameIdItemDTO, CategoryNameIdItemViewModel>();
        }
    }
}
