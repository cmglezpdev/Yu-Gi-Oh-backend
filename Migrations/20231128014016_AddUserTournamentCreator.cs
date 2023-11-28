using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTournamentCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "tournaments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tournaments_UserId",
                table: "tournaments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tournaments_users_UserId",
                table: "tournaments",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tournaments_users_UserId",
                table: "tournaments");

            migrationBuilder.DropIndex(
                name: "IX_tournaments_UserId",
                table: "tournaments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tournaments");
        }
    }
}
