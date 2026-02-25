using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.Models.ShippingProviders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.Couriers
{
    public class Courier
    {
        public int Id { get; set; }

        
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int ShippingProviderId { get; set; }
        public virtual ShippingProvider ShippingProvider { get; set; }

        public bool IsActive { get; set; } = true;
        public virtual ICollection<Shipment> Shipments { get; set; }
            =new HashSet<Shipment>(); 
    }
}
