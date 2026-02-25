using Enterprise_E_Commerce_Management_System.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.Products.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        //public enProductStatus Status { get; set; }
        //public DateOnly CreateDate { get; set; }
        public int CategoryId { get; set; }
    }
}
