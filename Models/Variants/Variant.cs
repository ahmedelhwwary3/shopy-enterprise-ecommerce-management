using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues;
using Enterprise_E_Commerce_Management_System.Models.Variants.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.Variants
{
    /// <summary>
    /// Specific Copy Of Product In Store
    /// </summary>
    public class Variant
    {
        public int Id { get; set; }

        public int ProductId {  get; set; }
        public virtual Product Product { get; set; } 

        public decimal Price {  get; set; }
        public decimal Cost {  get; set; }
        public int StockQuantity { get; set; }
        public string SKU {  get; set; }

        [NotMapped]
        public decimal Profit { get => Price - Cost;}

        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
            = new HashSet<AttributeValue>();

        public void MarkAsDeleted()
        {
            this.IsActive = false;
            this.IsDeleted = true;
        }
        
    }
}
