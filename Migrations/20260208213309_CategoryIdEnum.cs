using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class CategoryIdEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "DisplayType",
                table: "VariantAttributes",
                type: "tinyint",
                nullable: false,
                comment: "Text = 1,  Dropdown = 2, Color = 3,  Radio = 4,  Checkbox = 5",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayType",
                table: "VariantAttributes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldComment: "Text = 1,  Dropdown = 2, Color = 3,  Radio = 4,  Checkbox = 5");
        }
    }
}
