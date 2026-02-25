

namespace Enterprise_E_Commerce_Management_System.Application.Products.DTOs
{
    public class ProductItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateDate { get; set; }
    }
}
