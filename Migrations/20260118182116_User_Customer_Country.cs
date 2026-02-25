using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class User_Customer_Country : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 1);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "ApplicationPermissions",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "Module",
                value: "User");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "Module",
                value: "User");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "Module",
                value: "User");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "Module",
                value: "User");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "Module",
                value: "Role");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "Module",
                value: "Role");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "Module",
                value: "Role");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "Module",
                value: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CountryId",
                table: "Orders",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CountryId",
                table: "Customers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Countries_CountryId",
                table: "Customers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Countries_CountryId",
                table: "Orders",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Countries_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Countries_CountryId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Countries_CountryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CountryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CountryId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CountryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "ApplicationPermissions",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "Module",
                value: "AppicaionUser");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "Module",
                value: "AppicaionUser");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "Module",
                value: "AppicaionUser");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "Module",
                value: "AppicaionUser");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "Module",
                value: "AppicationRole");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "Module",
                value: "AppicationRole");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "Module",
                value: "AppicationRole");

            migrationBuilder.UpdateData(
                table: "ApplicationPermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "Module",
                value: "AppicationRole");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");
        }
    }
}
