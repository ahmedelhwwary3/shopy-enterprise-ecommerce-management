using Enterprise_E_Commerce_Management_System.Models.Products;

namespace Enterprise_E_Commerce_Management_System.Application.Products.DTOs
{
    public class UpdateProductDTO
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public int CategoryId { get; set; }
    }
}
