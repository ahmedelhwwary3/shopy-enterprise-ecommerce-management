using Enterprise_E_Commerce_Management_System.Application.OrderReturns.DTOs;

namespace Enterprise_E_Commerce_Management_System.ViewModels.OrderReturn
{
    public class OrderReturnPagedListViewModel
    {
        public OrderReturnFilterViewModel? Filter { get; set; }
        public List<OrderReturnItemViewModel>? OrderReturns { get; set; }
        public int Count { get; set; }
        public string? CurrencyCode { get; set; }
    }
}
