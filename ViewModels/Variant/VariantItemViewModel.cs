using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.Variant
{
    public class VariantItemViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Profit;
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public string Status {  get; set; }
    }
}
