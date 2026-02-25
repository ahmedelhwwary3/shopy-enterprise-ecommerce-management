using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class OrderReturns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Countries_CountryId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Orders",
                newName: "Address_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CountryId",
                table: "Orders",
                newName: "IX_Orders_Address_CountryId");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Shipments",
                type: "tinyint",
                nullable: false,
                comment: "NotShipped = 1,Processing = 2,Shipped = 3,Returned = 4",
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: " NotShipped = 1\nProcessing = 2,\nShipped = 3,\nInTransit = 4,\nDelivered = 5,\nReturned = 6");

            migrationBuilder.AlterColumn<int>(
                name: "Address_CountryId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Orders",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OrderReturns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, comment: " Requested = 1, PickedUp = 2, Returned = 3, Rejected = 4"),
                    CourierId = table.Column<int>(type: "int", nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickedUpAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderReturns_Couriers_CourierId",
                        column: x => x.CourierId,
                        principalTable: "Couriers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderReturns_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccessToken",
                table: "Orders",
                column: "AccessToken",
                unique: true,
                filter: "[AccessToken] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderReturns_CourierId",
                table: "OrderReturns",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderReturns_OrderId",
                table: "OrderReturns",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Countries_Address_CountryId",
                table: "Orders",
                column: "Address_CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Countries_Address_CountryId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderReturns");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AccessToken",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Address_CountryId",
                table: "Orders",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Address_CountryId",
                table: "Orders",
                newName: "IX_Orders_CountryId");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Shipments",
                type: "tinyint",
                nullable: false,
                comment: " NotShipped = 1\nProcessing = 2,\nShipped = 3,\nInTransit = 4,\nDelivered = 5,\nReturned = 6",
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "NotShipped = 1,Processing = 2,Shipped = 3,Returned = 4");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Countries_CountryId",
                table: "Orders",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
