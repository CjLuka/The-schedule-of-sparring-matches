using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addednewrelationbetweenclubanduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClubId",
                table: "Users",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clubs_ClubId",
                table: "Users",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clubs_ClubId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClubId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Users");
        }
    }
}
