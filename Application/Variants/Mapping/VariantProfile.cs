using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Variants.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Variants;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using Enterprise_E_Commerce_Management_System.ViewModels.Variant;

namespace Enterprise_E_Commerce_Management_System.Application.Variants.Mapping
{
    public class VariantProfile:Profile
    {
        public VariantProfile()
        {
            CreateMap<VariantFormViewModel, Variant>()
                .ForMember(v => v.Id, options => options.Ignore())
                .ForMember(v => v.IsActive, options => options.Ignore())
                .ForMember(v => v.IsDeleted, options => options.Ignore())
                .ForMember(v => v.AttributeValues, options => options.Ignore());

            CreateMap<Variant, VariantItemDTO>();

            CreateMap<VariantItemDTO, VariantItemViewModel>()
                .ForMember(vm=>vm.Status,options=>options.MapFrom(dto=>dto.IsActive?"Active": "Inactive"));

            CreateMap<VariantListDTO, VariantListViewModel>();

        }
    }
}
