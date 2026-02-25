using Enterprise_E_Commerce_Management_System.Models.Couriers;

namespace Enterprise_E_Commerce_Management_System.Models.ShippingProviders
{
    public class ShippingProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Courier> Couriers { get; set; }
            = new HashSet<Courier>();
    }
}
