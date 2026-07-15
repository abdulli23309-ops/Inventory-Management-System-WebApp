using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPurchaseDetailForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Purchases_PurchaseId1",
                table: "PurchaseDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseDetails_PurchaseId1",
                table: "PurchaseDetails");

            migrationBuilder.DropColumn(
                name: "PurchaseId1",
                table: "PurchaseDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseId1",
                table: "PurchaseDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_PurchaseId1",
                table: "PurchaseDetails",
                column: "PurchaseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Purchases_PurchaseId1",
                table: "PurchaseDetails",
                column: "PurchaseId1",
                principalTable: "Purchases",
                principalColumn: "Id");
        }
    }
}
