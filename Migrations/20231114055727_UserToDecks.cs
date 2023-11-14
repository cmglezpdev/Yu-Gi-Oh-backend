using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UserToDecks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "decks");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "decks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_decks_UserId",
                table: "decks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_decks_users_UserId",
                table: "decks",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_decks_users_UserId",
                table: "decks");

            migrationBuilder.DropIndex(
                name: "IX_decks_UserId",
                table: "decks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "decks");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "decks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
