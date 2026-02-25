namespace Enterprise_E_Commerce_Management_System.ViewModels.Shipment
{
    public class AssignedShipmentPagedListViewModel
    {
        public int Count { get; set; }
        public List<AssignedShipmentListItemViewModel>?Orders { get; set; }
        public AssignedShipmentFilterViewModel? Filter {  get; set; }
        public string? CurrencyCode { get; set; }
    }
}
