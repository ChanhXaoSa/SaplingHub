using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SH_DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class LinkAuctionToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuctionBids",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionBids_UserId",
                table: "AuctionBids",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionBids_AspNetUsers_UserId",
                table: "AuctionBids",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionBids_AspNetUsers_UserId",
                table: "AuctionBids");

            migrationBuilder.DropIndex(
                name: "IX_AuctionBids_UserId",
                table: "AuctionBids");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuctionBids",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
