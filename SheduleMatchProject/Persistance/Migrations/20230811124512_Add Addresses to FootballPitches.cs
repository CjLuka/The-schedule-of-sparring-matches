﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressestoFootballPitches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FootballPitches_Addresses_AddressesId",
                table: "FootballPitches");

            migrationBuilder.AlterColumn<int>(
                name: "AddressesId",
                table: "FootballPitches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FootballPitches_Addresses_AddressesId",
                table: "FootballPitches",
                column: "AddressesId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FootballPitches_Addresses_AddressesId",
                table: "FootballPitches");

            migrationBuilder.AlterColumn<int>(
                name: "AddressesId",
                table: "FootballPitches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FootballPitches_Addresses_AddressesId",
                table: "FootballPitches",
                column: "AddressesId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}