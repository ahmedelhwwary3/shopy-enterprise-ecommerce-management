using Enterprise_E_Commerce_Management_System.Application.Categories.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Shipments.DTOs;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.ShippingProviders;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.ShippingProviders
{
    public class ShippingProviderRepository : Repository<ShippingProvider>,IShippingProviderRepository
    {
        public ShippingProviderRepository(CommerceDbContext context) : base(context) { }
 
        public async Task<ICollection<ShippingProviderNameIdDTO>> GetAllAsync()
        {
            return await _context.ShippingProviders.Select(p => new ShippingProviderNameIdDTO()
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
        }

        public async Task<ShippingProviderPagedListDTO> GetListDtoAsync(ShippingProviderFilterDTO filter)
        {
            var providersCount = await _context.ShippingProviders
                .AsNoTracking()
                .Where(p =>
                (!filter.IsActive.HasValue || p.IsActive == filter.IsActive.Value) &&
                (string.IsNullOrEmpty(filter.Search) || p.Name.Contains(filter.Search)))
                .CountAsync();

            var pagedProviderList = await _context.ShippingProviders
                .AsNoTracking()
                .Where(p=>
                (!filter.IsActive.HasValue || p.IsActive==filter.IsActive.Value)&&
                (string.IsNullOrEmpty(filter.Search) || p.Name.Contains(filter.Search.Trim())))
                .Select(p => new ShippingProviderItemDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    CouriersCount = p.Couriers.Count
                })
                .OrderByDescending(p => p.CouriersCount)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var listDTO = new ShippingProviderPagedListDTO()
            {
                Count = providersCount,
                Providers = pagedProviderList,
                Filter=filter
            }; 
            return listDTO;
        }

        public async Task<bool> ExistsByNameAsync(string Name)
        {
            return await _context.ShippingProviders.AnyAsync(p=>p.Name==Name);
        }

        public async Task<ShippingProvider> GetTrackedByNameAsync(string Name)
        {
            return await _context.ShippingProviders
                .FirstOrDefaultAsync(p=>p.Name==Name);
        }
    }
}
