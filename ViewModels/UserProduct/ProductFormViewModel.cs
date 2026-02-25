using Enterprise_E_Commerce_Management_System.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.UserProducts
{
    public class ProductFormViewModel
    { 
       
        public List<CategoryNameIdItemViewModel>? Categories { get; set; }

        [Range(1,int.MaxValue,
            ErrorMessage = "Invalid Category Id.")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// For Edit Mode
        /// </summary>
        [Range(1,int.MaxValue,
            ErrorMessage ="Invalid Id")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Length(2, 30)]
        [Remote(action: "CheckUniqueName",
            controller: "Product",
            AdditionalFields =nameof(CategoryId) +","+nameof(Id),
            ErrorMessage = "There is already product with same name for this category.")]
        public string Name { get; set; }

        [Length(10, 500)]
        public string? Description { get; set; }

        [Remote(action: "ValidateImage", controller: "Product",
         ErrorMessage = "You must select image with format (.jpg, .jpeg, .png) .")]
        public IFormFile? ImageFile { get; set; } 
        public string? ImageName { get; set; }
    }
}
