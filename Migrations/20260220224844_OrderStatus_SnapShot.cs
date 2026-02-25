using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class OrderStatus_SnapShot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Shipments",
                newName: "ShipmentStatus");

            migrationBuilder.AddColumn<byte>(
                name: "OrderStatus",
                table: "Shipments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Pending=1, Paid=2,   InDelivery=3,   Cancelled=4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "ShipmentStatus",
                table: "Shipments",
                newName: "Status");
        }
    }
}
