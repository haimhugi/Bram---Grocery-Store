using Microsoft.EntityFrameworkCore.Migrations;

namespace Bram___grocery_store.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Sale_SaleId",
                table: "Category");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Category_SaleId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaleId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    SaleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_SaleId",
                table: "Category",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Sale_SaleId",
                table: "Category",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
