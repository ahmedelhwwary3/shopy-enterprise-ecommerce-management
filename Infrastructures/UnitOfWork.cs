
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
using Enterprise_E_Commerce_Management_System.Migrations;
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
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel;
using System.Threading.Tasks;
using attr = Enterprise_E_Commerce_Management_System.Models.Attributes;

namespace Enterprise_E_Commerce_Management_System.Infrastructures
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CommerceDbContext _context;
        private IDbContextTransaction _transaction;
        public UnitOfWork(CommerceDbContext context)//DIP (IOC)
        {
            _context=context;
            //All Custom Repos
            Carts = new CartRepository(_context);
            CartItems = new CartItemRepository(_context);
            Coupons = new Repository<Coupon>(_context);
            Customers = new CustomerRepository (_context);
            Orders = new OrderRepository(_context);
            OrderItems = new Repository<OrderItem>(_context);
            Payments = new PaymentRepository(_context);
            Products = new ProductRepository(_context);
            Shipments = new ShipmentRepository(_context);
            Variants = new VariantRepository(_context);
            Reviews = new Repository<Review>(_context);
            Categories=new Repository<Category>(_context);
            Countries=new Repository<Country>(_context);
            Couriers=new CourierRepository(_context);
            Attributes = new AttributeRepository(_context);
            ShippingProviders = new ShippingProviderRepository(_context);
            OrderReturns=new OrderReturnRepository(_context);
            Currencies = new CurrencyRepository(_context);
            CategoryAttributes = new CategoryAttributeRepository(_context);
            AttributeValues = new AttributeValueRepository(_context);

            ApplicationRoles = new Repository<ApplicationRole>(_context);
            ApplicationUserPermissions = new UserPermissionRepository(_context);
            ApplicationRolePermissions = new RolePermissionRepository(_context);
            ApplicationUserRoles = new UserRoleRepository(_context);
            ApplicationPermissions = new PermissionRepository(_context);
            ApplicationUsers=new UserRepository(_context);
        }
        public ICurrencyRepository Currencies { get; }
        public IRepository<Country> Countries { get; }
        public IRepository<Category> Categories { get; }
        public ICartRepository Carts { get; }
        public ICartItemRepository CartItems { get; }
        public IRepository<Coupon> Coupons { get; }
        public ICustomerRepository Customers { get; }
        public IOrderRepository Orders { get; }
        public IOrderReturnRepository OrderReturns { get; }
        public IRepository<OrderItem> OrderItems { get; }
        public IAttributeRepository Attributes { get; }
        public IPaymentRepository Payments { get; }
        public IProductRepository Products { get; }
        public IShipmentRepository Shipments { get; }
        public IVariantRepository Variants { get; }
        public IRepository<Review> Reviews { get; }
        public ICourierRepository Couriers { get; }
        public IShippingProviderRepository ShippingProviders { get; }
        public IAttributeValueRepository AttributeValues { get; }
        public ICategoryAttributeRepository CategoryAttributes { get; }


        public IRepository<ApplicationRole> ApplicationRoles { get; }
        public IUserPermissionRepository ApplicationUserPermissions { get; }
        public IUserRepository ApplicationUsers { get; }
        public IRolePermissionRepository ApplicationRolePermissions { get; } 
        public IPermissionRepository ApplicationPermissions { get; }
        public IUserRoleRepository ApplicationUserRoles { get; }  

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

    }
}
