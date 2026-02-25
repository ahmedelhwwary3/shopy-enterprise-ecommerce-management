using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DiscountType = table.Column<byte>(type: "tinyint", nullable: false, comment: "Percentage = 1,\nFixedAmount = 2,\nFreeShipping = 3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRolePermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ApplicationRolePermissions_ApplicationPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ApplicationPermissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationRolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    IsGranted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPermissions_ApplicationPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ApplicationPermissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUserPermissions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, comment: "Active = 1,\nInactive = 2"),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName_MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FullName_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_CountryId = table.Column<int>(type: "int", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, comment: "InActive=0, Active = 1"),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Country_Address_CountryId",
                        column: x => x.Address_CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.CheckConstraint("CK_Variant_Cost", "[Cost] BETWEEN 1 AND 999999999999");
                    table.CheckConstraint("CK_Variant_Price", "[Price] BETWEEN 1 AND 999999999999");
                    table.CheckConstraint("CK_Variant_StockQuantity", "[StockQuantity] BETWEEN 1 AND 2147483647");
                    table.ForeignKey(
                        name: "FK_Variants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, comment: "Inactive=0, Active=1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<byte>(type: "tinyint", nullable: false, comment: "New=1,\nPaid=2,\nShipped=3,\nCompleted=4\nCancelled=5"),
                    CouponId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.CheckConstraint("CK_Order_TotalAmount", "[TotalAmount] BETWEEN 1 AND 999999999999");
                    table.ForeignKey(
                        name: "FK_Orders_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.CheckConstraint("CK_Review_Comment", "LEN([Comment]) > 1");
                    table.CheckConstraint("CK_Review_Rating", "[Rating] BETWEEN 1 AND 5");
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.CheckConstraint("CK_CartItem_Quantity", "[UnitPrice] BETWEEN 1 AND 999999999999");
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.CheckConstraint("CK_OrderItem_Price", "[Price] BETWEEN 1 AND 999999999999");
                    table.CheckConstraint("CK_OrderItem_Quantity", "[Quantity] BETWEEN 1 AND 2147483647");
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<byte>(type: "tinyint", nullable: false, comment: "CashOnDelivery = 1,\nCreditCard = 2,\nWallet = 3"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<byte>(type: "tinyint", nullable: false, comment: "\nPending = 1,\nPaid = 2,\nFailed = 3,\nRefunded = 4"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.CheckConstraint("CK_Payment_Amount", "[Amount] BETWEEN 1 AND 999999999999");
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ShippingProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingStatus = table.Column<byte>(type: "tinyint", nullable: false, comment: " NotShipped = 1\nProcessing = 2,\nShipped = 3,\nInTransit = 4,\nDelivered = 5,\nReturned = 6"),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "Id", "Code", "Description", "IsActive", "Module" },
                values: new object[,]
                {
                    { 1, "Order.View", "View orders", true, "Order" },
                    { 2, "Order.Create", "Create new order", true, "Order" },
                    { 3, "Order.Edit", "Edit order", true, "Order" },
                    { 4, "Order.Cancel", "Cancel order", true, "Order" },
                    { 5, "Order.ChangeStatus", "Change order status", true, "Order" },
                    { 6, "Customer.View", "View customers", true, "Customer" },
                    { 7, "Customer.Create", "Create customer", true, "Customer" },
                    { 8, "Customer.Edit", "Edit customer", true, "Customer" },
                    { 9, "Customer.ToggleStatus", "Activate or deactivate customer", true, "Customer" },
                    { 10, "Product.View", "View products", true, "Product" },
                    { 11, "Product.Create", "Create product", true, "Product" },
                    { 12, "Product.Edit", "Edit product", true, "Product" },
                    { 13, "Product.ToggleStatus", "Activate or deactivate product", true, "Product" },
                    { 14, "User.View", "View users", true, "AppicaionUser" },
                    { 15, "User.Create", "Create user", true, "AppicaionUser" },
                    { 16, "User.Edit", "Edit user", true, "AppicaionUser" },
                    { 17, "User.ToggleStatus", "Activate or deactivate user", true, "AppicaionUser" },
                    { 18, "Role.View", "View roles", true, "AppicationRole" },
                    { 19, "Role.Create", "Create role", true, "AppicationRole" },
                    { 20, "Role.Edit", "Edit role and permissions", true, "AppicationRole" },
                    { 21, "Role.AssignPermissions", "Assign permissions to role", true, "AppicationRole" },
                    { 22, "Permission.View", "View system permissions", true, "Permission" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111", "3e6f9c4a-7d2b-4c8f-9a41-8c1f2b7a01a1", "Admin", "ADMIN" },
                    { "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222", "91b4e2d7-5f3c-4a1b-8e62-0a9c3d4f02b2", "Manager", "MANAGER" },
                    { "b8f9f7e2-9f01-4d6b-9b63-1a7c5d333333", "c7a1f3e9-8b6d-4c52-9d14-6f2e8a3b03c3", "Support", "SUPPORT" },
                    { "b8f9f7e2-9f01-4d6b-9b63-1a7c5d444444", "f2d9a8b4-1e7c-4f63-8a25-b1c6e4d904d4", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDeleted", "Name", "Status" },
                values: new object[,]
                {
                    { 1, false, "General", (byte)1 },
                    { 2, false, "Electronics", (byte)1 },
                    { 3, false, "Accessories", (byte)1 },
                    { 4, false, "Archived", (byte)2 }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Afghanistan" },
                    { 2, "Albania" },
                    { 3, "Algeria" },
                    { 4, "Andorra" },
                    { 5, "Angola" },
                    { 6, "Argentina" },
                    { 7, "Armenia" },
                    { 8, "Australia" },
                    { 9, "Austria" },
                    { 10, "Azerbaijan" },
                    { 11, "Bahrain" },
                    { 12, "Bangladesh" },
                    { 13, "Belarus" },
                    { 14, "Belgium" },
                    { 15, "Bolivia" },
                    { 16, "Brazil" },
                    { 17, "Bulgaria" },
                    { 18, "Canada" },
                    { 19, "Chile" },
                    { 20, "China" },
                    { 21, "Colombia" },
                    { 22, "Croatia" },
                    { 23, "Cuba" },
                    { 24, "Cyprus" },
                    { 25, "Czech Republic" },
                    { 26, "Denmark" },
                    { 27, "Dominican Republic" },
                    { 28, "Ecuador" },
                    { 29, "Egypt" },
                    { 30, "El Salvador" },
                    { 31, "Estonia" },
                    { 32, "Ethiopia" },
                    { 33, "Finland" },
                    { 34, "France" },
                    { 35, "Georgia" },
                    { 36, "Germany" },
                    { 37, "Ghana" },
                    { 38, "Greece" },
                    { 39, "Hungary" },
                    { 40, "Iceland" },
                    { 41, "India" },
                    { 42, "Indonesia" },
                    { 43, "Iran" },
                    { 44, "Iraq" },
                    { 45, "Ireland" },
                    { 46, "Italy" },
                    { 47, "Japan" },
                    { 48, "Jordan" },
                    { 49, "Kazakhstan" },
                    { 50, "Kenya" },
                    { 51, "Kuwait" },
                    { 52, "Latvia" },
                    { 53, "Lebanon" },
                    { 54, "Libya" },
                    { 55, "Lithuania" },
                    { 56, "Malaysia" },
                    { 57, "Mexico" },
                    { 58, "Morocco" },
                    { 59, "Netherlands" },
                    { 60, "New Zealand" },
                    { 61, "Nigeria" },
                    { 62, "Norway" },
                    { 63, "Oman" },
                    { 64, "Pakistan" },
                    { 65, "Palestine" },
                    { 66, "Peru" },
                    { 67, "Philippines" },
                    { 68, "Poland" },
                    { 69, "Portugal" },
                    { 70, "Qatar" },
                    { 71, "Romania" },
                    { 72, "Russia" },
                    { 73, "Saudi Arabia" },
                    { 74, "Serbia" },
                    { 75, "Singapore" },
                    { 76, "Slovakia" },
                    { 77, "South Africa" },
                    { 78, "Spain" },
                    { 79, "Sudan" },
                    { 80, "Sweden" },
                    { 81, "Switzerland" },
                    { 82, "Syria" },
                    { 83, "Thailand" },
                    { 84, "Tunisia" },
                    { 85, "Turkey" },
                    { 86, "Ukraine" },
                    { 87, "United Arab Emirates" },
                    { 88, "United Kingdom" },
                    { 89, "United States" },
                    { 90, "Venezuela" },
                    { 91, "Vietnam" },
                    { 92, "Yemen" },
                    { 93, "Zambia" },
                    { 94, "Zimbabwe" },
                    { 95, "Bahamas" },
                    { 96, "Barbados" },
                    { 97, "Belize" },
                    { 98, "Benin" },
                    { 99, "Bhutan" },
                    { 100, "Bosnia and Herzegovina" },
                    { 101, "Botswana" },
                    { 102, "Brunei" },
                    { 103, "Burkina Faso" },
                    { 104, "Burundi" },
                    { 105, "Cambodia" },
                    { 106, "Cameroon" },
                    { 107, "Cape Verde" },
                    { 108, "Central African Republic" },
                    { 109, "Chad" },
                    { 110, "Comoros" },
                    { 111, "Congo" },
                    { 112, "Costa Rica" },
                    { 113, "Côte d'Ivoire" },
                    { 114, "Djibouti" },
                    { 115, "Equatorial Guinea" },
                    { 116, "Eritrea" },
                    { 117, "Fiji" },
                    { 118, "Gabon" },
                    { 119, "Gambia" },
                    { 120, "Grenada" },
                    { 121, "Guatemala" },
                    { 122, "Guinea" },
                    { 123, "Guinea-Bissau" },
                    { 124, "Guyana" },
                    { 125, "Haiti" },
                    { 126, "Honduras" },
                    { 127, "Jamaica" },
                    { 128, "Kiribati" },
                    { 129, "Kyrgyzstan" },
                    { 130, "Laos" },
                    { 131, "Lesotho" },
                    { 132, "Liberia" },
                    { 133, "Liechtenstein" },
                    { 134, "Luxembourg" },
                    { 135, "Madagascar" },
                    { 136, "Malawi" },
                    { 137, "Maldives" },
                    { 138, "Mali" },
                    { 139, "Malta" },
                    { 140, "Marshall Islands" },
                    { 141, "Mauritania" },
                    { 142, "Mauritius" },
                    { 143, "Micronesia" },
                    { 144, "Moldova" },
                    { 145, "Monaco" },
                    { 146, "Mongolia" },
                    { 147, "Montenegro" },
                    { 148, "Mozambique" },
                    { 149, "Myanmar" },
                    { 150, "Namibia" },
                    { 151, "Nauru" },
                    { 152, "Nepal" },
                    { 153, "Nicaragua" },
                    { 154, "Niger" },
                    { 155, "North Korea" },
                    { 156, "North Macedonia" },
                    { 157, "Panama" },
                    { 158, "Papua New Guinea" },
                    { 159, "Paraguay" },
                    { 160, "Rwanda" },
                    { 161, "Saint Lucia" },
                    { 162, "Samoa" },
                    { 163, "San Marino" },
                    { 164, "Sao Tome and Principe" },
                    { 165, "Senegal" },
                    { 166, "Seychelles" },
                    { 167, "Sierra Leone" },
                    { 168, "Slovenia" },
                    { 169, "Solomon Islands" },
                    { 170, "Somalia" },
                    { 171, "South Sudan" },
                    { 172, "Sri Lanka" },
                    { 173, "Suriname" },
                    { 174, "Eswatini" },
                    { 175, "Tajikistan" },
                    { 176, "Tanzania" },
                    { 177, "Timor-Leste" },
                    { 178, "Togo" },
                    { 179, "Tonga" },
                    { 180, "Trinidad and Tobago" },
                    { 181, "Turkmenistan" },
                    { 182, "Tuvalu" },
                    { 183, "Uganda" },
                    { 184, "Uruguay" },
                    { 185, "Uzbekistan" },
                    { 186, "Vanuatu" },
                    { 187, "Vatican City" },
                    { 188, "Western Sahara" },
                    { 189, "Saint Vincent and the Grenadines" },
                    { 190, "Antigua and Barbuda" },
                    { 191, "Dominica" },
                    { 192, "Saint Kitts and Nevis" },
                    { 193, "South Korea" },
                    { 194, "Taiwan" },
                    { 195, "Kosovo" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationRolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 2, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 3, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 4, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 5, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 6, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 7, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 8, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 9, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 10, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 11, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 12, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 13, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 14, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 15, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 16, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 17, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 18, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 19, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 20, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 21, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 22, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d111111" },
                    { 1, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 2, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 3, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 4, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 5, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 6, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 8, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 9, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 10, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 11, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 12, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 13, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 14, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 16, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 17, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d222222" },
                    { 1, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d333333" },
                    { 3, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d333333" },
                    { 4, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d333333" },
                    { 6, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d333333" },
                    { 8, "b8f9f7e2-9f01-4d6b-9b63-1a7c5d333333" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissions_PermissionId",
                table: "ApplicationRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPermissions_PermissionId",
                table: "ApplicationUserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPermissions_UserId",
                table: "ApplicationUserPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_VariantId",
                table: "CartItems",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Address_CountryId",
                table: "Customers",
                column: "Address_CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_VariantId",
                table: "OrderItems",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerId",
                table: "Payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId",
                table: "Variants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_SKU",
                table: "Variants",
                column: "SKU",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRolePermissions");

            migrationBuilder.DropTable(
                name: "ApplicationUserPermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "ApplicationPermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
