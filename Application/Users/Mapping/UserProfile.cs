using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Users.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.User;

namespace Enterprise_E_Commerce_Management_System.Application.Users.Mapping
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserItemDTO,UserItemViewModel>()
                .ForMember(vm=>vm.Status,options=>options.MapFrom(dto=>dto.IsActive?"Active":"Inactive"));
            CreateMap<UserPagedListDTO,UserPagedListViewModel>();
        }
    }
}
