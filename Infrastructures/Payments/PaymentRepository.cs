using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Payments
{
    public class PaymentRepository : Repository<Payment>,IPaymentRepository
    {
        public PaymentRepository(CommerceDbContext context) : base(context) { }

        public async Task<bool> ExistsByOrderIdAsync(int orderId)
        {
            return await _context.Payments.AnyAsync(p=>p.OrderId==orderId);
        }

       
        
    }
}
