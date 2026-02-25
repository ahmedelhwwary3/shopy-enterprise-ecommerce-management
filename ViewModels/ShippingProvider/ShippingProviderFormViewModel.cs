using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.ShippingProvider
{
    public class ShippingProviderFormViewModel
    {
        [Range(1,int.MaxValue,
            ErrorMessage = "Invalid Id.")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Remote("CheckUniqueName", 
            "ShippingProvider",
            AdditionalFields =nameof(Id),
            ErrorMessage = "There is already Shipping Provider with same name .")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
