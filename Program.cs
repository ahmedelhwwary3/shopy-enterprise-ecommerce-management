
using Enterprise_E_Commerce_Management_System.Application.Attributes;
using Enterprise_E_Commerce_Management_System.Application.AttributeValues;
using Enterprise_E_Commerce_Management_System.Application.CartItems;
using Enterprise_E_Commerce_Management_System.Application.Carts;
using Enterprise_E_Commerce_Management_System.Application.Categories;
using Enterprise_E_Commerce_Management_System.Application.CategoryAttributes;
using Enterprise_E_Commerce_Management_System.Application.Countries;
using Enterprise_E_Commerce_Management_System.Application.Couriers;
using Enterprise_E_Commerce_Management_System.Application.Currencies;
using Enterprise_E_Commerce_Management_System.Application.Customers;
using Enterprise_E_Commerce_Management_System.Application.Emails;
using Enterprise_E_Commerce_Management_System.Application.OrderItems;
using Enterprise_E_Commerce_Management_System.Application.OrderReturns;
using Enterprise_E_Commerce_Management_System.Application.Orders; 
using Enterprise_E_Commerce_Management_System.Application.Payments;
using Enterprise_E_Commerce_Management_System.Application.Permissions;
using Enterprise_E_Commerce_Management_System.Application.Products;
using Enterprise_E_Commerce_Management_System.Application.RolePermissions;
using Enterprise_E_Commerce_Management_System.Application.Roles;
using Enterprise_E_Commerce_Management_System.Application.Shipments;
using Enterprise_E_Commerce_Management_System.Application.ShippingProviders;
using Enterprise_E_Commerce_Management_System.Application.ShoppingProducts;
using Enterprise_E_Commerce_Management_System.Application.UserPermissions; 
using Enterprise_E_Commerce_Management_System.Application.UserRoles;
using Enterprise_E_Commerce_Management_System.Application.Users;
using Enterprise_E_Commerce_Management_System.Application.Variants;
using Enterprise_E_Commerce_Management_System.Infrastructures;
using Enterprise_E_Commerce_Management_System.Infrastructures.Attributes;
using Enterprise_E_Commerce_Management_System.Infrastructures.AttributeValues;
using Enterprise_E_Commerce_Management_System.Infrastructures.CartItems;
using Enterprise_E_Commerce_Management_System.Infrastructures.Carts;
using Enterprise_E_Commerce_Management_System.Infrastructures.CategoryAttributes;
using Enterprise_E_Commerce_Management_System.Infrastructures.Countries;
using Enterprise_E_Commerce_Management_System.Infrastructures.Couriers;
using Enterprise_E_Commerce_Management_System.Infrastructures.Currencies;
using Enterprise_E_Commerce_Management_System.Infrastructures.Customers;
using Enterprise_E_Commerce_Management_System.Infrastructures.OrderReturns;
using Enterprise_E_Commerce_Management_System.Infrastructures.Orders;
using Enterprise_E_Commerce_Management_System.Infrastructures.Payments;
using Enterprise_E_Commerce_Management_System.Infrastructures.Permissions;
using Enterprise_E_Commerce_Management_System.Infrastructures.Products;
using Enterprise_E_Commerce_Management_System.Infrastructures.Shipments;
using Enterprise_E_Commerce_Management_System.Infrastructures.ShippingProviders;
using Enterprise_E_Commerce_Management_System.Infrastructures.UserPermissions;
using Enterprise_E_Commerce_Management_System.Infrastructures.UserRoles;
using Enterprise_E_Commerce_Management_System.Infrastructures.Users;
using Enterprise_E_Commerce_Management_System.Models.Permissions;
using Enterprise_E_Commerce_Management_System.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Enterprise_E_Commerce_Management_System
{
    
    public class Program
    {
        private static void HandleWebAppBuilderSettings(WebApplicationBuilder builder,string connection)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());//POST/PUT/DELETE
            });

            builder.Services.AddAuthorization(options =>
            {
                var permissionList = storage.GetPermissionCodePolicies();
                foreach (var value in permissionList)
                {
                    options.AddPolicy(value, policy => policy
                    .RequireClaim(storage.PermissionKey, value));
                }

            });
            builder.Services.AddCookiePolicy(options =>
            {
                // ???? ?? JavaScript ?? ????? ??????
                // ????? ?????? ?? XSS
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;

                // ?????? ????? ??? ?? HTTPS
                // ???? ??????? ???????? (Production)
                options.Secure = CookieSecurePolicy.Always;

                // ???? ?????? ?????? ?? ???????? ??????? ???? ??????
                // ????? ???? ????? CSRF
                // ????? ??????? ????? ???? ????????
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.LoginPath = "/Account/Login";
                options.SlidingExpiration = true;//Each Request Refresh Cookie Expiration 
            });
            //UserManager,RoleManager,SignInManger are injected automatically by DIP using this Service:-
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // ---------------- Password Settings ----------------
                options.Password.RequireDigit = true;            // At least one numeric character
                options.Password.RequireLowercase = true;        // At least one lowercase letter
                options.Password.RequireUppercase = true;        // At least one uppercase letter
                options.Password.RequireNonAlphanumeric = false; // Symbols are optional (better UX)
                options.Password.RequiredLength = 8;             // Minimum password length
                options.Password.RequiredUniqueChars = 1;        // Minimum number of unique characters

                // ---------------- Lockout Settings ----------------
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // Account lock duration after max failures
                options.Lockout.MaxFailedAccessAttempts = 5;       // Max failed login attempts before lockout
                options.Lockout.AllowedForNewUsers = true;         // Apply lockout rules to newly created users

                // ---------------- User Settings ----------------
                options.User.RequireUniqueEmail = true;            // Enforces unique email addresses per user (checked during user creation)

            })
            .AddEntityFrameworkStores<CommerceDbContext>();
            builder.Services.AddDbContext<CommerceDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connection));

            builder.Services.AddScoped<IApplicationRoleService, ApplicationRoleService>();
            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
            builder.Services.AddScoped<ICourierService, CourierService>();
            builder.Services.AddScoped<IApplicationUserPermissionService, ApplicationUserPermissionService>();
            builder.Services.AddScoped<IApplicationUserRoleService, ApplicationUserRoleService>();
            builder.Services.AddScoped<IApplicationRolePermissionService, ApplicationRolePermissionService>();
            builder.Services.AddScoped<IApplicationPermissionService, ApplicationPermissionService>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICategoryAttributeService, CategoryAttributeService>();
            builder.Services.AddScoped<IShippingProviderService, ShippingProviderService>();
            builder.Services.AddScoped<IShipmentService, ShipmentService>();
            builder.Services.AddScoped<IEmailSerivce, EmailSerivce>();
            builder.Services.AddScoped<IAttributeService, AttributeService>();
            builder.Services.AddScoped<IAttributeValueService, AttributeValueService>();
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddScoped<IShipmentOrderService, ShipmentOrderService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IShoppingService, ShoppingService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IVariantService, VariantService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ICartItemService, CartItemService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderReturnService, OrderReturnService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<ICartService, CartService>();

            builder.Services.AddScoped<ICourierQuery, CourierQuery>();
            builder.Services.AddScoped<IUserQuery, UserQuery>();
            builder.Services.AddScoped<ICartQuery, CartQuery>();
            builder.Services.AddScoped<IOrderQuery, OrderQuery>();
            builder.Services.AddScoped<IOrderReturnQuery, OrderReturnQuery>();
            builder.Services.AddScoped<IPaymentQuery, PaymentQuery>();
            builder.Services.AddScoped<IShipementQuery, ShipementQuery>();
            builder.Services.AddScoped<ICustomerQuery, CustomerQuery>();
            builder.Services.AddScoped<ICountryQuery, CountryQuery>();
            builder.Services.AddScoped<IProductQuery, ProductQuery>();

            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>(); 
            builder.Services.AddScoped<ICourierRepository, CourierRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<ICategoryAttributeRepository, CategoryAttributeRepository>(); 
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
            builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IAttributeRepository, AttributeRepository>();
            builder.Services.AddScoped<IOrderReturnRepository, OrderReturnRepository>();
            builder.Services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
            builder.Services.AddScoped<IVariantRepository, VariantRepository>();
            builder.Services.AddScoped<IShippingProviderRepository, ShippingProviderRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
            builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Registers AutoMapper and scans the current assembly for mapping profiles.
            // Automatically configures and injects IMapper via dependency injection.
            builder.Services.AddAutoMapper(typeof(Program));
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connection = builder.Configuration.GetConnectionString("CS");
            HandleWebAppBuilderSettings(builder, connection);
            var app = builder.Build();
         
            app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");//Custome Error Handling
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                ///Add Try Catch #####################################################################
                app.UseExceptionHandler("/Error/Index?statusCode=500");//Custome Exception Handling
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication(); 

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Shopping}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
