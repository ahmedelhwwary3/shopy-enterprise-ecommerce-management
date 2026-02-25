
using Enterprise_E_Commerce_Management_System.Application.Orders.Results;
using Enterprise_E_Commerce_Management_System.Models;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;
using Enterprise_E_Commerce_Management_System.ViewModels.Orders;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;

namespace Enterprise_E_Commerce_Management_System.Application.Orders
{
    public interface IOrderService
    {
        Task<enCreateOrderResult> CreateOrderAndSendTokenAsync(int customerId,int cartId);  
        Task<enAssignOrderForShipmentResult> AssignForShipmentAsync(int orderId, int courierId);
        Task<OrderManagementPagedListViewModel> GetUserAssignedListAsync(OrderManagementFilterViewModel filter,int currencyId);
        Task<OrderDetailsViewModel> GetDetailsViewModelByIdAsync(int orderId);
        Task<OrderTrackViewModel> GetTrackViewModelAsync(string AccessToken,int currencyId);
        Task<enCancelOrReturnOrderResult> CancelOrReturnAsync(int orderId, string notes);  
    }
}
