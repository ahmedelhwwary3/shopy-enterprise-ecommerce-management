using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;

namespace Enterprise_E_Commerce_Management_System.Application.Customers.Mapping
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerFormViewModel, Customer>()
                .ForMember(c => c.Id, options => options.Ignore())
                .ForMember(c => c.CreateDate, options => options.Ignore())
                .ForMember(c => c.IsActive, options => options.Ignore());

            CreateMap<Customer, CustomerFormViewModel>();

            CreateMap<CustomerItemDTO, CustomerItemViewModel>()
                .ForMember(vm=>vm.CreateDate,options=>options.MapFrom(dto=>DateOnly.FromDateTime(dto.CreateDate)))
                .ForMember(vm => vm.Status, options => options.MapFrom(dto => dto.IsActive?"Active":"Inactive"));
            
            CreateMap<CustomerPagedListDTO, CustomerPagedListViewModel>();
            CreateMap<CustomerFilterViewModel, CustomerFilterDTO>();
        }
    }
}
