using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHouseManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddressColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseUsers_Users_UserId",
                table: "HouseUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseUsers_Users_UserId",
                table: "HouseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseUsers_Users_UserId",
                table: "HouseUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseUsers_Users_UserId",
                table: "HouseUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
