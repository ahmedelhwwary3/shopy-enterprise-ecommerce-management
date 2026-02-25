using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.Customers.DTOs
{
    public class CustomerPagedListDTO
    {
        public int Count {  get; set; }

        [Required]
        public List<CountryNameIdViewModel> Countries { get; set; }

        [Required]
        public List<CustomerItemDTO> Customers { get; set; }
    }
}
