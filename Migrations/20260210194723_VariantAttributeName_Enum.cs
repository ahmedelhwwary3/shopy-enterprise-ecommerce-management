using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class VariantAttributeName_Enum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Name",
                table: "VariantAttributes",
                type: "tinyint",
                nullable: false,
                comment: " Color = 1,Size = 2,Storage = 3,Material = 4,Weight = 5,Capacity = 6,Length = 7,Width = 8,Height = 9,Volume = 10,Brand = 11,Model = 12",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "VariantAttributes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: " Color = 1,Size = 2,Storage = 3,Material = 4,Weight = 5,Capacity = 6,Length = 7,Width = 8,Height = 9,Volume = 10,Brand = 11,Model = 12");
        }
    }
}
