using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs
{
    public class ShoppingFilterDTO
    {
        public int Page { get; set; } = valid.DefaultPage;
        public int PageSize { get; set; } = valid.DefaultCartPageSize; 
        public int CurrencyId { get; set; } = valid.DollarCurrencyId;

        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice {  get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
