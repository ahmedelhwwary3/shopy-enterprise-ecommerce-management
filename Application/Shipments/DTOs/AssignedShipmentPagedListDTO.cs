using Enterprise_E_Commerce_Management_System.ViewModels.Order;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs
{
    public class AssignedShipmentPagedListDTO
    {
        public int Count { get; set; }
        public List<AssignedShipmentItemDTO>? Orders { get; set; }
        public AssignedShipmentFilterDTO? Filter { get; set; }
        public string CurrencyCode { get; set; }
    }
}
