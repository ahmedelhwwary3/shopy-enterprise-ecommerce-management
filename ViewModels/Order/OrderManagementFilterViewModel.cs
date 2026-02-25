using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Payments;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Order
{
    public class OrderManagementFilterViewModel
    { 
        public int Page { get; set; } = valid.DefaultPage;

        [Display(Name = "Page Size")]
        public int PageSize { get; set; } = valid.DefaultManagePageSize;
        public string? Search { get; set; } 

        [Display(Name = "Payment Status")]
        public enPaymentStatus? PaymentStatus { get; set; }

        [Display(Name = "Order Status")]
        public enOrderStatus? OrderStatus { get; set; }

        [Display(Name = "Payment Method")]
        public enPaymentMethod? PaymentMethod { get; set; }
    }
}
