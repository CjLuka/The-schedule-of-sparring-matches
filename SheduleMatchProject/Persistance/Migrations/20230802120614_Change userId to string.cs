using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeuserIdtostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchesClubs_AspNetUsers_UserId1",
                table: "BranchesClubs");

            migrationBuilder.DropIndex(
                name: "IX_BranchesClubs_UserId1",
                table: "BranchesClubs");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BranchesClubs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BranchesClubs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_BranchesClubs_UserId",
                table: "BranchesClubs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchesClubs_AspNetUsers_UserId",
                table: "BranchesClubs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchesClubs_AspNetUsers_UserId",
                table: "BranchesClubs");

            migrationBuilder.DropIndex(
                name: "IX_BranchesClubs_UserId",
                table: "BranchesClubs");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Clubs",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "BranchesClubs",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "BranchesClubs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BranchesClubs_UserId1",
                table: "BranchesClubs",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchesClubs_AspNetUsers_UserId1",
                table: "BranchesClubs",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
