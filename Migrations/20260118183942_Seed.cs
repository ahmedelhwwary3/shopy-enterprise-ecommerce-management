using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // =========================
            // ApplicationPermissions
            // =========================

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "Id", "Code", "Module", "Description", "IsActive" },
                values: new object[,]
                {
                    { 1, "Order.View", "Order", "View orders", true },
                    { 2, "Order.Create", "Order", "Create new order", true },
                    { 3, "Order.Edit", "Order", "Edit order", true },
                    { 4, "Order.Cancel", "Order", "Cancel order", true },
                    { 5, "Order.ChangeStatus", "Order", "Change order status", true },

                    { 6, "Customer.View", "Customer", "View customers", true },
                    { 7, "Customer.Create", "Customer", "Create customer", true },
                    { 8, "Customer.Edit", "Customer", "Edit customer", true },
                    { 9, "Customer.ToggleStatus", "Customer", "Activate or deactivate customer", true },

                    { 10, "Product.View", "Product", "View products", true },
                    { 11, "Product.Create", "Product", "Create product", true },
                    { 12, "Product.Edit", "Product", "Edit product", true },
                    { 13, "Product.ToggleStatus", "Product", "Activate or deactivate product", true },

                    { 14, "User.View", "User", "View users", true },
                    { 15, "User.Create", "User", "Create user", true },
                    { 16, "User.Edit", "User", "Edit user", true },
                    { 17, "User.ToggleStatus", "User", "Activate or deactivate user", true },

                    { 18, "Role.View", "Role", "View roles", true },
                    { 19, "Role.Create", "Role", "Create role", true },
                    { 20, "Role.Edit", "Role", "Edit role and permissions", true },
                    { 21, "Role.AssignPermissions", "Role", "Assign permissions to role", true },

                    { 22, "Permission.View", "Permission", "View system permissions", true }
                });

            // =========================
            // ApplicationRolePermissions
            // =========================
            // =========================
            // AspNetRoles (ApplicationRole)
            // =========================

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                          {
                 {
                     RoleIDs.Admin,
                     "Admin",
                     "ADMIN",
                     "3e6f9c4a-7d2b-4c8f-9a41-8c1f2b7a01a1"
                 },
                 {
                     RoleIDs.Manager,
                     "Manager",
                     "MANAGER",
                     "91b4e2d7-5f3c-4a1b-8e62-0a9c3d4f02b2"
                 },
                 {
                     RoleIDs.Support,
                     "Support",
                     "SUPPORT",
                     "c7a1f3e9-8b6d-4c52-9d14-6f2e8a3b03c3"
                 },
                 {
                     RoleIDs.Staff,
                     "Staff",
                     "STAFF",
                     "f2d9a8b4-1e7c-4f63-8a25-b1c6e4d904d4"
                 },
                 {
                     RoleIDs.Courier,
                     "Courier",
                     "COURIER",
                     "f2d9a8b4-1e7c-4f63-8a25-b1c6e2d000d4"
                 }
                       });
            migrationBuilder.InsertData(
                table: "ApplicationRolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                values: new object[,]
                {
                    // Manager
                    { RoleIDs.Manager, 1 },
                    { RoleIDs.Manager, 2 },
                    { RoleIDs.Manager, 3 },
                    { RoleIDs.Manager, 4 },
                    { RoleIDs.Manager, 5 },
                    { RoleIDs.Manager, 10 },
                    { RoleIDs.Manager, 11 },
                    { RoleIDs.Manager, 12 },
                    { RoleIDs.Manager, 13 },
                    { RoleIDs.Manager, 6 },
                    { RoleIDs.Manager, 8 },
                    { RoleIDs.Manager, 9 },
                    { RoleIDs.Manager, 14 },
                    { RoleIDs.Manager, 16 },
                    { RoleIDs.Manager, 17 },

                    // Support
                    { RoleIDs.Support, 1 },
                    { RoleIDs.Support, 3 },
                    { RoleIDs.Support, 4 },
                    { RoleIDs.Support, 6 },
                    { RoleIDs.Support, 8 },

                    // Courier
                    { RoleIDs.Courier, 1 },
                    { RoleIDs.Courier, 5 },
                    { RoleIDs.Courier, 6 }
                });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
