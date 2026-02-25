namespace Enterprise_E_Commerce_Management_System.Application.Orders
{
    public class OrderManagementPagedListDTO
    {
        public int Count { get; set; }
        public List<OrderManagementItemDTO> Orders { get; set; }
        public OrderManagementFilterDTO Filter { get; set; }
        public string CurrencyCode { get; set; }
    }
}
