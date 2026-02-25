namespace Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs
{
    public class CourierPagedListDTO
    {
        public CourierFilterDTO? Filter { get; set; }
        public List<CourierItemDTO>? Couriers { get; set; }
        public int Count { get; set; }
    }
}
