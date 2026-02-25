using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Countries.Mapping;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;

namespace Enterprise_E_Commerce_Management_System.Application.Customers.Mapping
{
    public class AddressProfile:Profile
    {
        public AddressProfile()
        {
            CreateMap<Address,AddressViewModel>().ReverseMap(); 
        }
    }
}
