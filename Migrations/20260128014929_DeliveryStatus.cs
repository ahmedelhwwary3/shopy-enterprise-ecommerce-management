using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Shipments",
                type: "tinyint",
                nullable: false,
                comment: "AssignedForDelivery = 1, Delivered = 2, DeliveryFailed = 3",
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "NotShipped = 1,Processing = 2,Shipped = 3,Returned = 4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Shipments",
                type: "tinyint",
                nullable: false,
                comment: "NotShipped = 1,Processing = 2,Shipped = 3,Returned = 4",
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "AssignedForDelivery = 1, Delivered = 2, DeliveryFailed = 3");
        }
    }
}
