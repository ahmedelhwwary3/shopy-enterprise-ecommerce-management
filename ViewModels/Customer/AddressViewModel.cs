using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Customer
{
    public class AddressViewModel
    {
        [Range(1,int.MaxValue, 
            ErrorMessage = "Invalid Country Id.")]
        [Display(Name ="Country")]
        public int CountryId { get; set; }
        public List<CountryNameIdViewModel>? Countries {  get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(20)]
        [DataType(DataType.PostalCode)]
        [Display(Name ="Postal Code")]
        public string? PostalCode { get; set; }

    }
}
