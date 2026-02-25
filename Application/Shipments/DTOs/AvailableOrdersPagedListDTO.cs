using Enterprise_E_Commerce_Management_System.ViewModels.Order;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs
{
    public class AvailableOrdersPagedListDTO
    {
        public AvailableOrderFilterDTO Filter { get; set; }
        public int Count { get; set; }
        public List<AvailableOrderItemDTO>? Orders { get; set; }
        public string CurrencyCode { get; set; }

    }
}
