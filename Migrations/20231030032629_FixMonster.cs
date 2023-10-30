using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixMonster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_monsters_CardId",
                table: "monsters",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_monsters_cards_CardId",
                table: "monsters",
                column: "CardId",
                principalTable: "cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_monsters_cards_CardId",
                table: "monsters");

            migrationBuilder.DropIndex(
                name: "IX_monsters_CardId",
                table: "monsters");
        }
    }
}
