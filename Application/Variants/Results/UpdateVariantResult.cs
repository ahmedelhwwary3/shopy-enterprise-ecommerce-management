namespace Enterprise_E_Commerce_Management_System.Application.Variants.Results
{
    public enum enUpdateVariantResult
    {
        VariantNotFound = 1, 
        NotUniqueSKU = 2,   
        NotUniqueAttributes=3,
        Success = 4,
        InvalidAttributeName=5
    }
}
