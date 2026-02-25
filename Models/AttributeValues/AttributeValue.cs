using attr= Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Variants;

namespace Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues
{
    public class AttributeValue
    {
        public int VariantId { get; set; }
        public virtual Variant Variant  { get; set; }

        public int AttributeId { get; set; }
        public virtual attr.Attribute Attribute { get; set; }

        public string Value { get; set; }
    }
}
