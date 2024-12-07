using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Data.identity.Migrations
{
    /// <inheritdoc />
    public partial class IdentitymigrationsFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_AppuserId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AppuserId",
                table: "Addresses",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AppuserId",
                table: "Addresses",
                newName: "IX_Addresses_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Addresses",
                newName: "AppuserId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AppUserId",
                table: "Addresses",
                newName: "IX_Addresses_AppuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_AppuserId",
                table: "Addresses",
                column: "AppuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
