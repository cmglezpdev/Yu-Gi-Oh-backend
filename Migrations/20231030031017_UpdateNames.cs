using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_monsters_cards_CardId",
                table: "monsters");

            migrationBuilder.DropIndex(
                name: "IX_monsters_CardId",
                table: "monsters");

            migrationBuilder.RenameColumn(
                name: "Rice",
                table: "monsters",
                newName: "Race");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Race",
                table: "monsters",
                newName: "Rice");

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
    }
}
