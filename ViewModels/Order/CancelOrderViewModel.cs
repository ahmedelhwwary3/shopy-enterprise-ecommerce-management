using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Order
{
    public class CancelOrderViewModel
    {
        [Range(1,int.MaxValue,
            ErrorMessage ="Invalid Order Id.")]
        public int OrderId { get; set; } 
        public string? Notes { get; set; }
    }
}
