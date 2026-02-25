using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Attributes.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts;

namespace Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.Mapping
{
    public class ShoppingProfile : Profile
    {
        public ShoppingProfile()
        {
            CreateMap<ShoppingItemDTO, ShoppingItemViewModel>();

            CreateMap<VariantAttributeShoppingItemDTO, ShoppingVariantAttributeItemViewModel>();

            CreateMap<ShoppingVariantItemDTO, ShoppingVarianttemViewModel>();

            CreateMap<ShoppingtDetailsDTO, ShoppingtDetailsViewModel>();
        }
    }
}
