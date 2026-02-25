namespace Enterprise_E_Commerce_Management_System.Application.Products.DTOs
{
    public class ProductPagedListDTO
    {
        public List<ProductItemDTO> Products { get; set; }
        public int Count { get; set; }
    }
}
