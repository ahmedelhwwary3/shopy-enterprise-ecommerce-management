namespace Enterprise_E_Commerce_Management_System.ViewModels.Shipment
{
    public class AvailableOrderPagedListViewModel
    {
        public int Count { get; set; }
        public AvailableOrderFilterViewModel? Filter { get; set; }
        public List<AvailableOrderItemViewModel>? Orders { get; set; } 
        public string? CurrencyCode { get; set; }
    }
}
