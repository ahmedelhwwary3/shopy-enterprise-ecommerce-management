using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.ViewModels.Order;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Shipments
{
    /// <summary>
    /// Provides explicit, read-only queries designed for specific
    /// business and presentation scenarios. Queries return DTOs
    /// and are optimized for complex or performance-sensitive reads.
    /// </summary>
    public interface IShipementQuery
    {
        Task<AssignedShipmentPagedListDTO> 
            GetCourierAssignedOrderListViewModelAsync(AssignedShipmentFilterDTO filter,int currencyId);
        Task<AvailableOrdersPagedListDTO> GetAvailableOrdersForCourierAsync(AvailableOrderFilterDTO filter,int currencyId);
    }
}
