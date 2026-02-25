using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Delivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippedDate",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ShippingProvider",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "ShippingStatus",
                table: "Shipments",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedDate",
                table: "Shipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourierId",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredDate",
                table: "Shipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "OrderStatus",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                comment: "Pending=1,\nPaid=2,\nShipped=3,\nCompleted=4\nCancelled=5",
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "New=1,\nPaid=2,\nShipped=3,\nCompleted=4\nCancelled=5");

            migrationBuilder.CreateTable(
                name: "ShippingProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, comment: "1-Active , 0-Inactive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Couriers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Couriers_ShippingProviders_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "ShippingProviders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CourierId",
                table: "Shipments",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_ShippingProviderId",
                table: "Couriers",
                column: "ShippingProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Couriers_UserId",
                table: "Couriers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingProviders_Name",
                table: "ShippingProviders",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Couriers_CourierId",
                table: "Shipments",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Couriers_CourierId",
                table: "Shipments");

            migrationBuilder.DropTable(
                name: "Couriers");

            migrationBuilder.DropTable(
                name: "ShippingProviders");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_CourierId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "AssignedDate",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "DeliveredDate",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Shipments",
                newName: "ShippingStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippedDate",
                table: "Shipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ShippingProvider",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "OrderStatus",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                comment: "New=1,\nPaid=2,\nShipped=3,\nCompleted=4\nCancelled=5",
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "Pending=1,\nPaid=2,\nShipped=3,\nCompleted=4\nCancelled=5");
        }
    }
}
