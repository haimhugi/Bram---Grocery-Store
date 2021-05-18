using Microsoft.EntityFrameworkCore.Migrations;

namespace Bram___grocery_store.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Sales_CurrentSaleId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_CurrentSaleId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CurrentSaleId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "SalesId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Sales_SalesId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_SalesId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "CurrentSaleId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_CurrentSaleId",
                table: "Category",
                column: "CurrentSaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Sales_CurrentSaleId",
                table: "Category",
                column: "CurrentSaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
