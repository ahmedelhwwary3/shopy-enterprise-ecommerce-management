using Enterprise_E_Commerce_Management_System.Models.Attributes;

namespace Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts
{
    public class ShoppingVariantAttributeItemViewModel
    {
        //public int Id { get; set; }
        public int VariantId { get; set; }
        public string Value { get; set; }
        public enAttributeName Name { get; set; }
        public string NameText => Name.ToString();
        public enDisplayType DisplayType { get; set; }
    }
}
