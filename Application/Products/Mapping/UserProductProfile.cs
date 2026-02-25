using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Products.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.ViewModels.UserProducts;

namespace Enterprise_E_Commerce_Management_System.Application.Products.Mapping
{
    public class UserProductProfile:Profile
    {
        public UserProductProfile()
        {
            CreateMap<CreateProductDTO, Product>()
                .ForMember(prd => prd.Id, options => options.Ignore())
                .ForMember(prd => prd.IsActive, options => options.Ignore())
                .ForMember(prd => prd.CreateDate, options => options.Ignore());
            CreateMap<ProductItemDTO, ProductItemViewModel>()
                .ForMember(
                  dest => dest.CreateDate,
                  opt => opt.MapFrom(src => DateOnly.FromDateTime(src.CreateDate)))
                .ForMember(vm => vm.Status, options => options.MapFrom(dto => dto.IsActive ? "Active" : "Inactive"));

            CreateMap<ProductPagedListDTO, ProductPagedListViewModel>();
            CreateMap<ProductFormViewModel, CreateProductDTO>().ReverseMap();
            CreateMap<ProductFormViewModel, UpdateProductDTO>()
                .ForMember(dto=>dto.Description,options=>options.MapFrom(
                    vm=>string.IsNullOrWhiteSpace(vm.Description)?"":vm.Description));
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, ProductFormViewModel>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, ProductFormViewModel>().ReverseMap();
           

        }
    }
}
