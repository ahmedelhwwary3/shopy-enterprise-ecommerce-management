using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Customer
{
    public class CustomerPagedListViewModel
    {
        public int Count { get; set; }

        public CustomerFilterViewModel? Filter { get; set; } 
        public List<CountryNameIdViewModel>? Countries { get; set; } 
        public List<CustomerItemViewModel>? Customers { get; set; }
    }
}
