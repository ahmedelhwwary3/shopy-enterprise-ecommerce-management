namespace Enterprise_E_Commerce_Management_System.Application.OrderReturns.DTOs
{
    public class OrderReturnPagedListDTO
    {
        public OrderReturnFilterDTO? Filter { get; set; }
        public List<OrderReturnItemDTO>? OrderReturns { get; set; }
        public int Count { get; set; }
        public string CurrencyCode { get; set; }
    }
}
