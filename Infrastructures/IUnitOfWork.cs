using Enterprise_E_Commerce_Management_System.Infrastructures.Attributes;
using Enterprise_E_Commerce_Management_System.Infrastructures.AttributeValues;
using Enterprise_E_Commerce_Management_System.Infrastructures.CartItems;
using Enterprise_E_Commerce_Management_System.Infrastructures.Carts;
using Enterprise_E_Commerce_Management_System.Infrastructures.CategoryAttributes;
using Enterprise_E_Commerce_Management_System.Infrastructures.Couriers;
using Enterprise_E_Commerce_Management_System.Infrastructures.Currencies;
using Enterprise_E_Commerce_Management_System.Infrastructures.Customers;
using Enterprise_E_Commerce_Management_System.Infrastructures.OrderReturns;
using Enterprise_E_Commerce_Management_System.Infrastructures.Orders;
using Enterprise_E_Commerce_Management_System.Infrastructures.Payments;
using Enterprise_E_Commerce_Management_System.Infrastructures.Permissions;
using Enterprise_E_Commerce_Management_System.Infrastructures.Products;
using Enterprise_E_Commerce_Management_System.Infrastructures.RolePermissions;
using Enterprise_E_Commerce_Management_System.Infrastructures.Shipments;
using Enterprise_E_Commerce_Management_System.Infrastructures.ShippingProviders;
using Enterprise_E_Commerce_Management_System.Infrastructures.UserPermissions;
using Enterprise_E_Commerce_Management_System.Infrastructures.UserRoles;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Countries;
using Enterprise_E_Commerce_Management_System.Models.Coupons;
using Enterprise_E_Commerce_Management_System.Models.OrderItems;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.Models.Reviews;
using Enterprise_E_Commerce_Management_System.Models.Shipments;
using Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using attr = Enterprise_E_Commerce_Management_System.Models.Attributes;
using ctg = Enterprise_E_Commerce_Management_System.Models.CategoryAttributes;

namespace Enterprise_E_Commerce_Management_System.Infrastructures
{
    public interface IUnitOfWork
    {
        IRepository<Country> Countries { get; }
        ICourierRepository Couriers { get; }
        IRepository<Category> Categories { get; }
        IRepository<Review> Reviews { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderReturnRepository OrderReturns { get; }
        ICartRepository Carts { get; }
        ICartItemRepository CartItems { get; }
        IRepository<Coupon> Coupons { get; }
        ICustomerRepository Customers { get; }
        IRepository<OrderItem> OrderItems { get; }
        IAttributeRepository Attributes { get; }
        IPaymentRepository Payments { get; } 
        IShippingProviderRepository ShippingProviders { get; }
        IShipmentRepository Shipments { get; }
        IVariantRepository Variants { get; }
        ICurrencyRepository Currencies { get; }
        IAttributeValueRepository AttributeValues { get; }
        ICategoryAttributeRepository CategoryAttributes { get; } 


        IPermissionRepository ApplicationPermissions { get; }
        IUserRoleRepository ApplicationUserRoles { get; }
        IUserPermissionRepository ApplicationUserPermissions { get; }
        IRolePermissionRepository ApplicationRolePermissions { get; }
        IUserRepository ApplicationUsers { get; }
        IRepository<ApplicationRole> ApplicationRoles { get; }


        #region Managed By Identity
        //IRoleRepository ApplicationRoles { get; }
        //IRepository<ApplicationUser> ApplicationUsers { get; }
        //IRoleRepository Roles { get; } 
        #endregion
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
