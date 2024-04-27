using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class ecommthird : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phoneNumber",
                table: "Addresses",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "houseNumber",
                table: "Addresses",
                newName: "HouseNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Addresses",
                newName: "phoneNumber");

            migrationBuilder.RenameColumn(
                name: "HouseNumber",
                table: "Addresses",
                newName: "houseNumber");
        }
    }
}
