namespace Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts
{
    /// <summary>
    /// RangeValue Proberty equals MinPrice-MaxPrice
    /// </summary>
    public class ProductPriceItemViewModel
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string Label { get; set; }
        public string RangeValue => $"{MinPrice}-{MaxPrice}";
    }
}
