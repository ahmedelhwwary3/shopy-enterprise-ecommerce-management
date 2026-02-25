using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Enterprise_E_Commerce_Management_System.Models.Carts
{
    public class Cart
    {
        public int Id {  get; set; }

        public int? CustomerId {  get; set; }
        public virtual Customer? Customer { get; set; }

        public DateTime CreateDate { get; set; }
        public bool IsActive {  get; set; } = true;    

        public virtual ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems=new HashSet<CartItem>();
        }

    }
}
