using Enterprise_E_Commerce_Management_System.Models.Attributes;

namespace Enterprise_E_Commerce_Management_System.Application.Attributes.DTOs
{
    public class VariantAttributeShoppingItemDTO
    {
        //public int Id { get; set; }
        public int VariantId { get; set; }
        public string Value { get; set; }
        public enAttributeName Name { get; set; }
        public enDisplayType DisplayType { get; set; }

    }
}
