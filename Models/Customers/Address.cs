using Enterprise_E_Commerce_Management_System.Models.Countries;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Models.Customers
{
    [Owned]
    public class Address
    {
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public string City { get; set; }
        public string Street { get; set; }

        public string? PostalCode { get; set; }
    }
}
