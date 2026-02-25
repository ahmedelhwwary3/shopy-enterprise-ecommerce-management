using Enterprise_E_Commerce_Management_System.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.UserProducts
{
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public DateOnly CreateDate { get; set; }
    }
}
