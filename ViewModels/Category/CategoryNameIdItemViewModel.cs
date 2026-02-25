using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Category
{
    public class CategoryNameIdItemViewModel
    {
        [Range(1, int.MaxValue,
            ErrorMessage ="Invalid Id.")]
        public int Id { get; set; }

        [Required(ErrorMessage ="*")]
        public string Name { get; set; }
    }
}
