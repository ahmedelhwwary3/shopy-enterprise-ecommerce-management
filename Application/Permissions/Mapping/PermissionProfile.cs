using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Permissions.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Permission;

namespace Enterprise_E_Commerce_Management_System.Application.Permissions.Mapping
{
    public class PermissionProfile:Profile
    {
        public PermissionProfile()
        {
            CreateMap<PermissionItemDTO,PermissionItemViewModel>();
        }
    }
}
