using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enterprise_E_Commerce_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class VariantAttributes_NoColorSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Variants_ProductId_Color_Size",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Variants");

            migrationBuilder.CreateTable(
                name: "VariantAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantAttributes_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId",
                table: "Variants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantAttributes_VariantId_Name",
                table: "VariantAttributes",
                columns: new[] { "VariantId", "Name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantAttributes");

            migrationBuilder.DropIndex(
                name: "IX_Variants_ProductId",
                table: "Variants");

            migrationBuilder.AddColumn<byte>(
                name: "Color",
                table: "Variants",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "Black = 1,White = 2,Gray = 3,Red = 10,Blue = 11,Green = 12,Yellow = 13,Brown = 20,Beige = 21,Pink = 30,Purple = 31,Orange = 40,Navy = 41");

            migrationBuilder.AddColumn<byte>(
                name: "Size",
                table: "Variants",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "XS = 1,S = 2,M = 3,L = 4,XL = 5,XXL = 6,XXXL = 7");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ProductId_Color_Size",
                table: "Variants",
                columns: new[] { "ProductId", "Color", "Size" },
                unique: true);
        }
    }
}
