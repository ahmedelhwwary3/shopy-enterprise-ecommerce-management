using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class PriceRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Variant_Cost",
                table: "Variants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Variant_Price",
                table: "Variants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Payment_Amount",
                table: "Payments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_TotalAmount",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Price",
                table: "OrderItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CartItem_Quantity",
                table: "CartItems");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Variant_Cost",
                table: "Variants",
                sql: "[Cost] BETWEEN 0.001 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Variant_Price",
                table: "Variants",
                sql: "[Price] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Payment_Amount",
                table: "Payments",
                sql: "[Amount] BETWEEN 0.001 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_TotalAmount",
                table: "Orders",
                sql: "[TotalAmount] BETWEEN 0.001 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Price",
                table: "OrderItems",
                sql: "[Price] BETWEEN 0.001 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CartItem_Quantity",
                table: "CartItems",
                sql: "[UnitPrice] BETWEEN 0.001 AND 999999999999");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Variant_Cost",
                table: "Variants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Variant_Price",
                table: "Variants");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Payment_Amount",
                table: "Payments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_TotalAmount",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderItem_Price",
                table: "OrderItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CartItem_Quantity",
                table: "CartItems");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Variant_Cost",
                table: "Variants",
                sql: "[Cost] BETWEEN 1 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Variant_Price",
                table: "Variants",
                sql: "[Price] BETWEEN 1 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Payment_Amount",
                table: "Payments",
                sql: "[Amount] BETWEEN 1 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_TotalAmount",
                table: "Orders",
                sql: "[TotalAmount] BETWEEN 1 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderItem_Price",
                table: "OrderItems",
                sql: "[Price] BETWEEN 1 AND 999999999999");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CartItem_Quantity",
                table: "CartItems",
                sql: "[UnitPrice] BETWEEN 1 AND 999999999999");
        }
    }
}
