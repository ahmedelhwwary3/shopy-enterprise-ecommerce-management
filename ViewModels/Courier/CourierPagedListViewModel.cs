namespace Enterprise_E_Commerce_Management_System.ViewModels.Courier
{
    public class CourierPagedListViewModel
    {
        public CourierFilterViewModel? Filter { get; set; }
        public List<CourierItemViewModel>? Couriers { get; set; }
        public int Count { get; set; }
    }
}
