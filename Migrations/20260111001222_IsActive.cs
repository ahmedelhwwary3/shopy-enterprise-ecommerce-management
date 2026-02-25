using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class IsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Couriers");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Variants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ShippingProviders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Couriers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ShippingProviders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Couriers");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Variants",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "1-Active, 0-Inactive");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Products",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Active = 1,\nInactive = 2");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Couriers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "1-Active , 0-Inactive");
        }
    }
}
