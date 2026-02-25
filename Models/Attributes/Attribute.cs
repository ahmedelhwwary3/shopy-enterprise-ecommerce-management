using Enterprise_E_Commerce_Management_System.Models.Variants;

namespace Enterprise_E_Commerce_Management_System.Models.Attributes
{
    public class Attribute
    {
        public int Id { get; set; }
        public enDisplayType DisplayType { get; set; } = enDisplayType.Dropdown;
        public enAttributeName Name { get; set; } 

    }
}
