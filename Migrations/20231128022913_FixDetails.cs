using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerWinner",
                table: "duels",
                newName: "PlayerWinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_tournament_inscriptions_DeckId",
                table: "tournament_inscriptions",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_tournament_inscriptions_TournamentId",
                table: "tournament_inscriptions",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_tournament_inscriptions_UserId",
                table: "tournament_inscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_duels_PlayerAId",
                table: "duels",
                column: "PlayerAId");

            migrationBuilder.CreateIndex(
                name: "IX_duels_PlayerBId",
                table: "duels",
                column: "PlayerBId");

            migrationBuilder.CreateIndex(
                name: "IX_duels_PlayerWinnerId",
                table: "duels",
                column: "PlayerWinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_duels_TournamentId",
                table: "duels",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_duels_tournaments_TournamentId",
                table: "duels",
                column: "TournamentId",
                principalTable: "tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_duels_users_PlayerAId",
                table: "duels",
                column: "PlayerAId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_duels_users_PlayerBId",
                table: "duels",
                column: "PlayerBId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_duels_users_PlayerWinnerId",
                table: "duels",
                column: "PlayerWinnerId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tournament_inscriptions_decks_DeckId",
                table: "tournament_inscriptions",
                column: "DeckId",
                principalTable: "decks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tournament_inscriptions_tournaments_TournamentId",
                table: "tournament_inscriptions",
                column: "TournamentId",
                principalTable: "tournaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tournament_inscriptions_users_UserId",
                table: "tournament_inscriptions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_duels_tournaments_TournamentId",
                table: "duels");

            migrationBuilder.DropForeignKey(
                name: "FK_duels_users_PlayerAId",
                table: "duels");

            migrationBuilder.DropForeignKey(
                name: "FK_duels_users_PlayerBId",
                table: "duels");

            migrationBuilder.DropForeignKey(
                name: "FK_duels_users_PlayerWinnerId",
                table: "duels");

            migrationBuilder.DropForeignKey(
                name: "FK_tournament_inscriptions_decks_DeckId",
                table: "tournament_inscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_tournament_inscriptions_tournaments_TournamentId",
                table: "tournament_inscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_tournament_inscriptions_users_UserId",
                table: "tournament_inscriptions");

            migrationBuilder.DropIndex(
                name: "IX_tournament_inscriptions_DeckId",
                table: "tournament_inscriptions");

            migrationBuilder.DropIndex(
                name: "IX_tournament_inscriptions_TournamentId",
                table: "tournament_inscriptions");

            migrationBuilder.DropIndex(
                name: "IX_tournament_inscriptions_UserId",
                table: "tournament_inscriptions");

            migrationBuilder.DropIndex(
                name: "IX_duels_PlayerAId",
                table: "duels");

            migrationBuilder.DropIndex(
                name: "IX_duels_PlayerBId",
                table: "duels");

            migrationBuilder.DropIndex(
                name: "IX_duels_PlayerWinnerId",
                table: "duels");

            migrationBuilder.DropIndex(
                name: "IX_duels_TournamentId",
                table: "duels");

            migrationBuilder.RenameColumn(
                name: "PlayerWinnerId",
                table: "duels",
                newName: "PlayerWinner");
        }
    }
}
