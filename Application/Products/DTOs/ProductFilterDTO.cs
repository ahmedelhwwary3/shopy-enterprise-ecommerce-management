

namespace Enterprise_E_Commerce_Management_System.Application.Products.DTOs
{
    public class ProductFilterDTO
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;

        public string? Name { get; set; }
        public int? CategoryId { get; set; }

        public bool? IsActive { get; set; } = null;
    }
}
