using Enterprise_E_Commerce_Management_System.Application.Countries.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Couriers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Countries
{
    /// <summary>
    /// Provides explicit, read-only queries designed for specific
    /// business and presentation scenarios. Queries return DTOs
    /// and are optimized for complex or performance-sensitive reads.
    /// </summary>
    public interface ICourierQuery
    {
        Task<CourierPagedListDTO> GetListAsync(CourierFilterDTO filter);
    }
}
