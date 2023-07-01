using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FootballPitchIdnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_FootballPitches_FootballPitchId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchRequests_FootballPitches_FootballPitchId",
                table: "MatchRequests");

            migrationBuilder.AlterColumn<int>(
                name: "FootballPitchId",
                table: "MatchRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FootballPitchId",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_FootballPitches_FootballPitchId",
                table: "Matches",
                column: "FootballPitchId",
                principalTable: "FootballPitches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRequests_FootballPitches_FootballPitchId",
                table: "MatchRequests",
                column: "FootballPitchId",
                principalTable: "FootballPitches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_FootballPitches_FootballPitchId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchRequests_FootballPitches_FootballPitchId",
                table: "MatchRequests");

            migrationBuilder.AlterColumn<int>(
                name: "FootballPitchId",
                table: "MatchRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FootballPitchId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_FootballPitches_FootballPitchId",
                table: "Matches",
                column: "FootballPitchId",
                principalTable: "FootballPitches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchRequests_FootballPitches_FootballPitchId",
                table: "MatchRequests",
                column: "FootballPitchId",
                principalTable: "FootballPitches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
