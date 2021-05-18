using Microsoft.EntityFrameworkCore.Migrations;

namespace Bram___grocery_store.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Sales_SalesId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_SalesId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "SalesId",
                table: "Category",
                newName: "SaleId");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountPercentage",
                table: "Sales",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "MySaleId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_MySaleId",
                table: "Category",
                column: "MySaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Sales_MySaleId",
                table: "Category",
                column: "MySaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Sales_MySaleId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_MySaleId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "MySaleId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "Category",
                newName: "SalesId");

            migrationBuilder.AlterColumn<float>(
                name: "DiscountPercentage",
                table: "Sales",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Category_SalesId",
                table: "Category",
                column: "SalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Sales_SalesId",
                table: "Category",
                column: "SalesId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
