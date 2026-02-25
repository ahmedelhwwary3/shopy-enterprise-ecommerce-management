namespace Enterprise_E_Commerce_Management_System.Application.Orders.Results
{
    public enum enAssignOrderForShipmentResult
    {
        Success = 1, 
        OrderNotFound = 2, 
        OrderItemsNotFound = 3,
        OrderVariantNotFound = 4,
        PaidOrderReturnNotFound = 5,
        InvalidOrderStatus=6
    }
}
