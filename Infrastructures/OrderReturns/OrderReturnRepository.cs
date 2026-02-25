using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.OrderReturns
{
    public class OrderReturnRepository : Repository<OrderReturn>, IOrderReturnRepository
    {
        public OrderReturnRepository(CommerceDbContext context) : base(context) { }

        public async Task<OrderReturn> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderReturns
                .FirstOrDefaultAsync(r=>r.OrderId==orderId);
        }
    }
}
