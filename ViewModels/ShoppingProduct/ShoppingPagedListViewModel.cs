using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts
{
    public class ShoppingPagedListViewModel
    {
        [Required]
        public List<ShoppingItemViewModel> Products { get; set; }
        public string Code { get; set; }
        public int Count {  get; set; }
    }
}
