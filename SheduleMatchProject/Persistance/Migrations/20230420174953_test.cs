using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FootballPitches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballPitches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballPitches_Addresses_AddressesId",
                        column: x => x.AddressesId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clubs_GameClasses_GameClassId",
                        column: x => x.GameClassId,
                        principalTable: "GameClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FootballPitchRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStartEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    FootballPitchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballPitchRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FootballPitchRequests_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FootballPitchRequests_FootballPitches_FootballPitchId",
                        column: x => x.FootballPitchId,
                        principalTable: "FootballPitches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClubHomeId = table.Column<int>(type: "int", nullable: false),
                    ClubAwayId = table.Column<int>(type: "int", nullable: false),
                    GoalsHome = table.Column<int>(type: "int", nullable: false),
                    GoalsAway = table.Column<int>(type: "int", nullable: false),
                    FootballPitchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Clubs_ClubAwayId",
                        column: x => x.ClubAwayId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Matches_Clubs_ClubHomeId",
                        column: x => x.ClubHomeId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Matches_FootballPitches_FootballPitchId",
                        column: x => x.FootballPitchId,
                        principalTable: "FootballPitches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    FootballPitchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchRequests_Clubs_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchRequests_Clubs_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MatchRequests_FootballPitches_FootballPitchId",
                        column: x => x.FootballPitchId,
                        principalTable: "FootballPitches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_GameClassId",
                table: "Clubs",
                column: "GameClassId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballPitches_AddressesId",
                table: "FootballPitches",
                column: "AddressesId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballPitchRequests_ClubId",
                table: "FootballPitchRequests",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_FootballPitchRequests_FootballPitchId",
                table: "FootballPitchRequests",
                column: "FootballPitchId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ClubAwayId",
                table: "Matches",
                column: "ClubAwayId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ClubHomeId",
                table: "Matches",
                column: "ClubHomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FootballPitchId",
                table: "Matches",
                column: "FootballPitchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequests_FootballPitchId",
                table: "MatchRequests",
                column: "FootballPitchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequests_ReceiverId",
                table: "MatchRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequests_SenderId",
                table: "MatchRequests",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballPitchRequests");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "MatchRequests");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "FootballPitches");

            migrationBuilder.DropTable(
                name: "GameClasses");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
