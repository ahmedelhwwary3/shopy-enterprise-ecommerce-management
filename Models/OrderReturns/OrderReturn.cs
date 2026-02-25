using Enterprise_E_Commerce_Management_System.Models.Couriers;
using System;
using static Dapper.SqlMapper;

namespace Enterprise_E_Commerce_Management_System.Models.OrderReturns
{
    
    public class OrderReturn
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order  { get; set; }
        public enOrderReturnStatus Status { get; set; }
        public int? CourierId { get; set; }
        public virtual Courier? Courier { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Notes { get; set; } 
    }
}
