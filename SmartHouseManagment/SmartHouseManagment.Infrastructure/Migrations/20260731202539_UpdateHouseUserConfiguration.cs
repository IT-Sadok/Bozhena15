using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHouseManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHouseUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street2",
                table: "Houses",
                newName: "Address_Address2");

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Houses",
                newName: "Address_Address1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Address2",
                table: "Houses",
                newName: "Address_Street2");

            migrationBuilder.RenameColumn(
                name: "Address_Address1",
                table: "Houses",
                newName: "Address_Street");
        }
    }
}
