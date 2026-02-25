using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.UserProducts
{
    public class ProductPagedListViewModel
    {
        [Required]
        public  List<ProductItemViewModel> Products {  get; set; }
        public int Count {  get; set; }
    }
}
