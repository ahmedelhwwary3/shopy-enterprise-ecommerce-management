using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Courier_Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b7f9f7e1-9f01-4d6b-9b63-1a7c5d444444", "f2d9a8b4-1e7c-4f63-8a25-b1c6e2d000d4", "Courier", "COURIER" });

            migrationBuilder.InsertData(
                table: "ApplicationRolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, "b7f9f7e1-9f01-4d6b-9b63-1a7c5d444444" },
                    { 5, "b7f9f7e1-9f01-4d6b-9b63-1a7c5d444444" },
                    { 6, "b7f9f7e1-9f01-4d6b-9b63-1a7c5d444444" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationRolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, "b7f9f7e1-9f01-4d6b-9b63-1a7c5d444444" });

            migrationBuilder.DeleteData(
                table: "ApplicationRolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, "b7f9f7e1-9f01-4d6b-9b63-1a7c5d444444" });

            migrationBuilder.DeleteData(
                table: "ApplicationRolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 6, "b7f9f7e1-9f01-4d6b-9b63-1a7c5d444444" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7f9f7e1-9f01-4d6b-9b63-1a7c5d444444");
        }
    }
}
