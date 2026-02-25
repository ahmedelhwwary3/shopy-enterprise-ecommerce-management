using Enterprise_E_Commerce_Management_System.Models.Categories;

using Enterprise_E_Commerce_Management_System.Models.Reviews;
using System.ComponentModel.DataAnnotations; 
using Enterprise_E_Commerce_Management_System.Models.Variants;
using Enterprise_E_Commerce_Management_System.Application.Products.DTOs;

namespace Enterprise_E_Commerce_Management_System.Models.Products
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } 
        public bool IsActive { get; set; } = true;
        public DateOnly CreateDate { get; set; } 
        public int CategoryId {  get; set; }
        public virtual Category Category { get; set; } 
        public virtual ICollection<Variant>Variants { get; set; }
            =new HashSet<Variant>();
        public virtual ICollection<Review>Reviews { get; set; }
            = new HashSet<Review>();
        public bool IsDeleted { get; set; } = false;

        [Required]
        [MaxLength(150)]
        public string ImageName {  get; set; }

        public void MarkAsDeleted()
        {
            this.IsDeleted = true;
            this.IsActive = false;
        }
        public static bool operator ==(Product dbProduct, UpdateProductDTO dtoProduct)
        {
            if (ReferenceEquals(dbProduct, null) && ReferenceEquals(dtoProduct, null))
                return true;

            if (ReferenceEquals(dbProduct, null) || ReferenceEquals(dtoProduct, null))
                return false;
            return dbProduct.Name == dtoProduct.Name &&
                dbProduct.Id == dtoProduct.Id &&
                dbProduct.CategoryId == dtoProduct.CategoryId &&
                dtoProduct.Description == dtoProduct.Description;
        }
        public static bool operator !=(Product dbProduct, UpdateProductDTO dtoProduct)
        {
            if (ReferenceEquals(dbProduct, null) && ReferenceEquals(dtoProduct, null))
                return false;

            if (ReferenceEquals(dbProduct, null) || ReferenceEquals(dtoProduct, null))
                return true;
            return dbProduct.Name == dtoProduct.Name &&
                dbProduct.Id == dtoProduct.Id &&
                dbProduct.CategoryId == dtoProduct.CategoryId &&
                dtoProduct.Description == dtoProduct.Description;
        }
        public Product CopyValuesFromDTO(UpdateProductDTO dto)
        {
            this.Description = dto.Description??string.Empty;
            this.CategoryId = dto.CategoryId;
            this.Name = dto.Name;
            return this;
        }
    }
}
