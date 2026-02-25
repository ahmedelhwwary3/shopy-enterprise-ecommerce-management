using Enterprise_E_Commerce_Management_System.Application.Carts.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Currencies.DTOs; 
using Enterprise_E_Commerce_Management_System.Models;
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Infrastructures.Currencies
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(CommerceDbContext context):base(context) { }

        public async Task<List<CurrencyCodeIdItemDTO>> GetAllNameIdDTOAsync()
        {
            return await _context.Currencies
                .GroupBy(curr=>curr.Code) 
                .Select(grp => new CurrencyCodeIdItemDTO()
                {
                    Id = grp.First().Id,
                    Code = grp.Key
                }).ToListAsync();
        }

        public async Task<string> GetCodeByIdAsync(int Id)
        {
            return await _context.Currencies
                .Where(curr => curr.Id == Id)
                .Select(curr => curr.Code)
                .FirstOrDefaultAsync();
        }

        public async Task<decimal> GetDollarRateByIdAsync(int Id)
        {
            return await _context.Currencies
                .Where(curr => curr.Id == Id)
                .Select(curr => curr.DollarRate)
                .FirstOrDefaultAsync();
        }
    }
}
