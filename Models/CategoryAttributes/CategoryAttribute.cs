using Enterprise_E_Commerce_Management_System.Models.Categories;
using attr=Enterprise_E_Commerce_Management_System.Models.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise_E_Commerce_Management_System.Models.CategoryAttributes
{
    public class CategoryAttribute
    {

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int AttributeId { get; set; } 
        public virtual attr.Attribute Attribute { get; set; }
    }
}
