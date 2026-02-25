namespace Enterprise_E_Commerce_Management_System.Application.Products.Results
{
    public enum enUpdateProductResult
    {
        ProductNotFound = 1,
        CategoryNotFound = 2,
        NotUniqueName = 3,
        NotUniqueImageName=4,
        InvalidImage = 5, 
        Success = 6,
        NotUpdateMode=7
    }
}
