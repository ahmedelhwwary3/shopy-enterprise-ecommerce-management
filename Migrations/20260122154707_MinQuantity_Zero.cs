using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class MinQuantity_Zero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Variant_StockQuantity",
                table: "Variants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity",
                table: "OrderItems");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Variant_StockQuantity",
                table: "Variants",
                sql: "[StockQuantity] BETWEEN 0 AND 2147483647");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity",
                table: "OrderItems",
                sql: "[Quantity] BETWEEN 0 AND 2147483647");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Variant_StockQuantity",
                table: "Variants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Quantity",
                table: "OrderItems");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Variant_StockQuantity",
                table: "Variants",
                sql: "[StockQuantity] BETWEEN 1 AND 2147483647");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Quantity",
                table: "OrderItems",
                sql: "[Quantity] BETWEEN 1 AND 2147483647");
        }
    }
}
