using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Countries;

namespace Enterprise_E_Commerce_Management_System.Application.Customers.DTOs
{
    public class AddressDTO
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string? PostalCode { get; set; }
        //public CountryDTO Country {  get; set; }
        public int CountryId {  get; set; }
    }
}
