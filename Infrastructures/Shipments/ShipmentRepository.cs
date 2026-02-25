using Enterprise_E_Commerce_Management_System.Application.Orders.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Shipments
{
    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(CommerceDbContext context) : base(context) { }
        public async Task<ShipmentDetailsDTO> GetDetailsDtoByOrderIdAsync(int orderId,int currencyId)
        {
            var currency=await _context.Currencies
                .Where(curr => curr.Id == currencyId)
                .Select(curr =>new { curr.Code,curr.DollarRate})
                .FirstOrDefaultAsync();

            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == orderId)
                .Select(o => new ShipmentDetailsDTO()
                {
                    Id = o.Id,
                    CurrencyCode= currency.Code,
                    City = o.Customer.Address.City,
                    PostalCode = o.Customer.Address.PostalCode,
                    Street = o.Customer.Address.Street,
                    Country = o.Address.Country.Name,

                    OrderDate = o.OrderDate,
                    OrderNumber = o.OrderNumber,
                    Amount = o.TotalAmount * currency.DollarRate,
                    Status = o.OrderStatus,
                    PhoneNumber = o.Customer.PhoneNumber,

                    FullName = o.Customer.FullName.ToString()
                }).FirstOrDefaultAsync();
        }

        public async Task<Shipment> GetLastByOrderIdAsync(int orderId)
        {
            return await _context.Shipments
                .OrderBy(sh=>sh.Id)
                .LastOrDefaultAsync(s=>s.OrderId==orderId);
        }

        public async Task<ConfirmShipmentDTO> GetConfirmDtoByIdAsync(int shipmentId)
        {
            return await _context.Shipments
                .AsNoTracking()
                .Where(sh => sh.Id == shipmentId)
                .Select(sh => new ConfirmShipmentDTO()
                {
                    LastStatus = sh.ShipmentStatus,
                    FullName = sh.Order.Customer.FullName.ToString(),
                    City = sh.Order.Customer.Address.City,
                    Country = sh.Order.Customer.Address.Country.Name,
                    PostalCode = sh.Order.Customer.Address.PostalCode,
                    Street = sh.Order.Customer.Address.Street,
                    ShipmentId=shipmentId
                }).FirstOrDefaultAsync();
        }
    }
}
