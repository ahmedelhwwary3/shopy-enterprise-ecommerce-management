using AutoMapper;
using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Countries;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;

namespace Enterprise_E_Commerce_Management_System.Application.Countries.Mapping
{
    public class CountryProfile:Profile
    {
        public CountryProfile()
        {
            CreateMap<Country , CountryNameIdViewModel>();
            CreateMap<CountryNameIdDTO, CountryNameIdViewModel>().ReverseMap();
        }
    }
}
