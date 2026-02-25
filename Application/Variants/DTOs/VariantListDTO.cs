namespace Enterprise_E_Commerce_Management_System.Application.Variants.DTOs
{
    public class VariantListDTO
    {
        public List<VariantItemDTO> Variants { get; set; }
        public int Count { get; set; }
        public string CurrencyCode { get; set; }
    }
}
