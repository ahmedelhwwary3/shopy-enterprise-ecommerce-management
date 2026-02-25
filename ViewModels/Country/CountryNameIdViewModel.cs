using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Country
{
    public class CountryNameIdViewModel
    {
        [StringLength(20)]
        [Display(Name="Country Name")]
        public string? Name {  get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid Id.")] 
        public int Id { get; set; }
    }
}
