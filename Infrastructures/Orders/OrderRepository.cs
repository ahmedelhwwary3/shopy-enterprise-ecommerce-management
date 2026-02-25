using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Orders
{
    public class OrderRepository:Repository<Order>,IOrderRepository
    {
        public OrderRepository(CommerceDbContext context) : base(context) { }
         
        public async Task<Order> GetIncludeItemsByOrderIdAsync(int orderId)
        {
            return await _context.Orders
                .Where(i => i.Id == orderId)
                .AsTracking()
                .Include(i => i.OrderItems)
                .ThenInclude(v => v.Variant)
                .ThenInclude(v => v.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderDetailsDTO> GetDetailsDtoByIdAsync(int orderId)
        {
            var shipmentData = await _context.Shipments
                    .AsNoTracking()
                    .Where(s => s.OrderId == orderId)
                    .Select(s => new { s.CourierId, s.Courier.User.UserName, s.ShipmentStatus })
                    .FirstOrDefaultAsync();

            var paymentData = await _context.Payments
                    .AsNoTracking()
                    .Where(s => s.OrderId == orderId)
                    .Select(s => new { s.PaymentMethod, s.PaymentStatus })
                    .FirstOrDefaultAsync(); 

            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == orderId)
                .Select(o => new OrderDetailsDTO()
                {
                    OrderId = o.Id,
                    Amount = o.TotalAmount,
                    OrderDate = o.OrderDate,
                    OrderNumber = o.OrderNumber,
                    OrderStatus = o.OrderStatus,
                    PhoneNumber = o.Customer.PhoneNumber,
                    FullName = o.Customer.FullName.ToString(),
                    CourierId = o.Shipments.OrderBy(s => s.Id).Last().CourierId,
                    CourierUserName = o.Shipments.OrderBy(s => s.Id).Last().Courier.User.UserName,
                    ShipmentStatus = o.Shipments.OrderBy(s=>s.Id).Last().ShipmentStatus,
                    Country = o.Customer.Address.Country.Name,
                    City = o.Customer.Address.City,
                    PostalCode = o.Customer.Address.PostalCode,
                    Street = o.Customer.Address.PostalCode,
                    PaymentMethod = o.Payments.Count>1? o.Payments.OrderBy(p => p.Id).Last().PaymentMethod:null,
                    PaymentStatus = o.Payments.Count > 1 ? o.Payments.OrderBy(p => p.Id).Last().PaymentStatus : null
                }).FirstOrDefaultAsync();
        }

        public async Task<Order> GetByShipmentIdAsync(int shipmentId)
        {
            return await _context.Shipments
                .AsTracking()
                .Where(sh=>sh.Id==shipmentId)
                .Select(sh=>sh.Order)
                .FirstOrDefaultAsync();
        }

        public async Task<OrderTrackDTO> GetTrackDtoByTokenAsync(string token,int currencyId)
        {
            var currency = await _context.Currencies
                .Where(curr => curr.Id == currencyId)
                .Select(curr => new { curr.DollarRate, curr.Code })
                .FirstOrDefaultAsync();

            return await _context.Orders.Where(o => o.AccessToken == token)
                .AsNoTracking()
                .Select(o => new OrderTrackDTO()
                {
                    OrderId = o.Id,
                    Amount = o.TotalAmount * currency.DollarRate,
                    CurrencyCode= currency.Code,
                    OrderDate = o.OrderDate,
                    OrderNumber = o.OrderNumber,
                    OrderStatus = o.OrderStatus,
                    PhoneNumber = o.Customer.PhoneNumber,
                    FullName = o.Customer.FullName.ToString(),
                    Country = o.Customer.Address.Country.Name,
                    City = o.Customer.Address.City,
                    PostalCode = o.Customer.Address.PostalCode,
                    Street = o.Customer.Address.Street,
                    PaymentMethod = o.Payments != null ? o.Payments.OrderBy(p=>p.Id).Last().PaymentMethod : null,
                    PaymentStatus = o.Payments != null ? o.Payments.OrderBy(p => p.Id).Last().PaymentStatus : null,
                    ShipmentStatus = o.Shipments != null ? o.Shipments.OrderBy(sh => sh.Id).Last().ShipmentStatus : null
                })
                .FirstOrDefaultAsync();
        }
    }
}
