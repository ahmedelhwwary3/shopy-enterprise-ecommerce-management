using  attr= Enterprise_E_Commerce_Management_System.Models.CategoryAttributes;
using Enterprise_E_Commerce_Management_System.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Enterprise_E_Commerce_Management_System.Models.Categories
{
    public class Category
    {
        public int Id {  get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public int? ParentId { get; set; } = null;
        public virtual Category? Parent { get; set; } = null;

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
     
        public virtual ICollection<Product> Products { get; set; }
            =new HashSet<Product>();
        public virtual ICollection<attr.CategoryAttribute> CategoryAttributes { get; set; }
           = new HashSet<attr.CategoryAttribute>();
    }
}
