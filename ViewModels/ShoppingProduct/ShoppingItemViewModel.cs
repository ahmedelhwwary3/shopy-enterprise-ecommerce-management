using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts.DTOs;
using Enterprise_E_Commerce_Management_System.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts
{
    public class ShoppingItemViewModel
    {
        public int ProductId { get; set; }
        public string Name { set; get; }
        public string ImageName {  set; get; }
        public string CategoryName {  set; get; }
        public decimal MinPrice {  set; get; }
        public decimal MaxPrice {  set; get; }
        public string Status {  set; get; }
        public List<ShoppingVariantAttributeItemViewModel> Attributes { get; set; }
    }
}
