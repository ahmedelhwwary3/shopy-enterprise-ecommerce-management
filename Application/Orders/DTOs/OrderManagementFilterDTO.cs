using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Payments;

namespace Enterprise_E_Commerce_Management_System.Application.Orders
{
    public class OrderManagementFilterDTO
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; }=valid.DefaultManagePageSize;
        public string? Search { get; set; } 

        public enPaymentStatus? PaymentStatus { get; set; }
        public enOrderStatus? OrderStatus { get; set; }
        public enPaymentMethod? PaymentMethod { get; set; }
    }
}
