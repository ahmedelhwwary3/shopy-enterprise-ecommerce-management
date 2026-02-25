using Enterprise_E_Commerce_Management_System.ViewModels.Country;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Courier
{
    public class EditCouierViewModel
    {
        [Range(1,int.MaxValue,
            ErrorMessage ="Invalid Id.")]
        public int? Id { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Invalid Country Id.")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public List<CountryNameIdViewModel>? Countries { get; set; }


        [Range(1, int.MaxValue,
                    ErrorMessage = "Invalid Shipping Provider Id.")]
        [Display(Name = "Shipping Provider")] 
        public int ShippingProviderId { get; set; }
        public List<ShippingProviderIdNameViewModel>? Providers { get; set; }
    }
}
