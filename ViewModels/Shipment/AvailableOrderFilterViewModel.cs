using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Shipment
{
    public class AvailableOrderFilterViewModel
    {
        
        public int? CountryId { get; set; }

        public int Page {  get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
        public string? Search {  get; set; }
    }
}
