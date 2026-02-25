using Enterprise_E_Commerce_Management_System.Application.Shipments.Results;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Enterprise_E_Commerce_Management_System.ViewModels.Shipment;
using Microsoft.AspNetCore.Identity;

namespace Enterprise_E_Commerce_Management_System.Application.Shipments
{
    public interface IShipmentOrderService
    {
        Task<enRecalculateStockQuantityStatusResult> RecalculateStockQuantityAndStatusAsync(bool IsRemove, int orderId);
    }
}
