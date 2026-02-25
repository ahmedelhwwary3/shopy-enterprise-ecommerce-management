using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Customers; 
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;

namespace Enterprise_E_Commerce_Management_System.Application.Customers.Mapping
{
    public class FullNameProfile:Profile
    {
        public FullNameProfile()
        {
            CreateMap<FullNameDTO, FullNameViewModel>().ReverseMap();
            CreateMap<FullName, FullNameViewModel>().ReverseMap();
        }
    }
}
