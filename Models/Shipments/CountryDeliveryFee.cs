using Enterprise_E_Commerce_Management_System.Models.Countries;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Models.Shipments
{
    public class CountryDeliveryFee
    {
        public int Id { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }



        [Range(typeof(string),valid.MinPrice,valid.MaxPrice)]
        public decimal MinFee { get; set; }


        [Range(typeof(string), valid.MinPrice, valid.MaxPrice)]
        public decimal MaxFee { get; set; }


        public bool IsActive { get; set; } = true;
    }
}
