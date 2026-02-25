 
namespace Enterprise_E_Commerce_Management_System.ViewModels.Order
{
    public class OrderManagementPagedListViewModel
    {
        public OrderManagementFilterViewModel? Filter { get; set; }
        public List<OrderManagementItemViewModel>? Orders { get; set; }
        public int Count { get; set; }
        public string? CurrencyCode { get; set; }
    }
}
