namespace Enterprise_E_Commerce_Management_System.ViewModels.ShoppingProducts
{
    public class ShoppingtDetailsViewModel
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public string Code { get; set; }
        public List<ShoppingVarianttemViewModel>? Variants { get; set; }
    }
}
