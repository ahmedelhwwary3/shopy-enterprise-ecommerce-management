using Enterprise_E_Commerce_Management_System.Models;
using Enterprise_E_Commerce_Management_System.Models.ApplicationUserRoles;
using Enterprise_E_Commerce_Management_System.Models.CartItems;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Categories;
using Enterprise_E_Commerce_Management_System.Models.Countries;
using Enterprise_E_Commerce_Management_System.Models.Coupons;
using Enterprise_E_Commerce_Management_System.Models.Couriers;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Enterprise_E_Commerce_Management_System.Models.Reviews;
using Enterprise_E_Commerce_Management_System.Models.ShippingProviders;
using Enterprise_E_Commerce_Management_System.Models.Attributes;
using Enterprise_E_Commerce_Management_System.Models.Variants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using attr = Enterprise_E_Commerce_Management_System.Models.Attributes;
using ctg = Enterprise_E_Commerce_Management_System.Models.CategoryAttributes;
using Enterprise_E_Commerce_Management_System.Models.VariantAttributeValues;
using Enterprise_E_Commerce_Management_System.Models.OrderReturns;
using Enterprise_E_Commerce_Management_System.Models.OrderItems;

namespace Enterprise_E_Commerce_Management_System.Infrastructures
{
    public class CommerceDbContext:IdentityDbContext<ApplicationUser,ApplicationRole,string,
        IdentityUserClaim<string>, ApplicationUserRole,IdentityUserLogin<string>, 
        IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        //public CommerceDbContext() : base() { }
        public CommerceDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CommerceDbContext).Assembly);
            base.OnModelCreating(builder);
        }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Cart> Carts {  get; set; }
        public virtual DbSet<CartItem>CartItems { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders {  get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products {  get; set; }
        public virtual DbSet<Shipment>Shipments { get; set; }
        public virtual DbSet<Variant>Variants { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<ShippingProvider> ShippingProviders { get; set; }
        public virtual DbSet<CountryDeliveryFee> CountryDeliveryFees { get; set; }
        public virtual DbSet<OrderReturn> OrderReturns { get; set; }
        public virtual DbSet<attr.Attribute> Attributes { get; set; }
        public virtual DbSet<ctg.CategoryAttribute> CategoryAttributes { get; set; }
        public virtual DbSet<AttributeValue> AttributeValues { get; set; }


        public virtual DbSet<ApplicationUserPermission> ApplicationUserPermissions { get; set; }
        public virtual DbSet<ApplicationRolePermission> ApplicationRolePermissions { get; set; }
        public virtual DbSet<ApplicationPermission> ApplicationPermissions { get; set; }


        public override DbSet<ApplicationRole> Roles { get => base.Roles; set => base.Roles = value; }
        public override DbSet<ApplicationUser> Users { get => base.Users; set => base.Users = value; }
        public override DbSet<IdentityUserLogin<string>> UserLogins { get => base.UserLogins; set => base.UserLogins = value; }
        public override DbSet<IdentityRoleClaim<string>> RoleClaims { get => base.RoleClaims; set => base.RoleClaims = value; }
        public override DbSet<ApplicationUserRole> UserRoles { get => base.UserRoles; set => base.UserRoles = value; }
        public override DbSet<IdentityUserClaim<string>> UserClaims { get => base.UserClaims; set => base.UserClaims = value; } 
        public override DbSet<IdentityUserToken<string>> UserTokens { get => base.UserTokens; set => base.UserTokens = value; }

    }
}
