namespace Enterprise_E_Commerce_Management_System.Global.Constants
{
    /// <summary>
    /// System-defined Permission identifiers used for seeding and authorization.
    /// These values should never be changed once deployed.
    /// </summary>
    public enum enPermissions
    {
        // Account
        Account_RegisterUser = 1,
        Account_RegisterCourier = 2,

        // Couriers
        Couriers_Manage = 3,

        // Customers
        Customers_Manage = 4,

        // Orders
        Orders_UsersViewAssigned = 5,
        Orders_AcceptShipment = 6,

        // Order Returns
        OrderReturns_View = 7,

        // Payments
        Payments_View = 8,

        // Products
        Products_Manage = 9,

        // Roles
        Roles_Manage = 10,

        // Shipments
        Shipments_ViewAvailableOrders = 11,
        Shipments_ManageCourierOrders = 12,

        // Shipping Providers
        ShippingProviders_Manage = 13,

        // Users
        Users_Manage = 14,

        // Variants
        Variants_Manage = 15
    }



}
