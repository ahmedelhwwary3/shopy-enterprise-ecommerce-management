using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class EnumColorSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Size",
                table: "Variants",
                type: "tinyint",
                nullable: false,
                comment: "XS = 1,S = 2,M = 3,L = 4,XL = 5,XXL = 6,XXXL = 7",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<byte>(
                name: "Color",
                table: "Variants",
                type: "tinyint",
                nullable: false,
                comment: "Black = 1,White = 2,Gray = 3,Red = 10,Blue = 11,Green = 12,Yellow = 13,Brown = 20,Beige = 21,Pink = 30,Purple = 31,Orange = 40,Navy = 41",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Variants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "XS = 1,S = 2,M = 3,L = 4,XL = 5,XXL = 6,XXXL = 7");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Variants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "Black = 1,White = 2,Gray = 3,Red = 10,Blue = 11,Green = 12,Yellow = 13,Brown = 20,Beige = 21,Pink = 30,Purple = 31,Orange = 40,Navy = 41");
        }
    }
}
