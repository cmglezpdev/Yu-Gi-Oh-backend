using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_Archetypes_ArchetypeId",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_MonsterCards_cards_CardId",
                table: "MonsterCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Archetypes",
                table: "Archetypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonsterCards",
                table: "MonsterCards");

            migrationBuilder.RenameTable(
                name: "Archetypes",
                newName: "archetypes");

            migrationBuilder.RenameTable(
                name: "MonsterCards",
                newName: "monsters");

            migrationBuilder.RenameIndex(
                name: "IX_MonsterCards_CardId",
                table: "monsters",
                newName: "IX_monsters_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_archetypes",
                table: "archetypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_monsters",
                table: "monsters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_archetypes_ArchetypeId",
                table: "cards",
                column: "ArchetypeId",
                principalTable: "archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_cards_archetypes_ArchetypeId",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_monsters_cards_CardId",
                table: "monsters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_archetypes",
                table: "archetypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_monsters",
                table: "monsters");

            migrationBuilder.RenameTable(
                name: "archetypes",
                newName: "Archetypes");

            migrationBuilder.RenameTable(
                name: "monsters",
                newName: "MonsterCards");

            migrationBuilder.RenameIndex(
                name: "IX_monsters_CardId",
                table: "MonsterCards",
                newName: "IX_MonsterCards_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archetypes",
                table: "Archetypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonsterCards",
                table: "MonsterCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_Archetypes_ArchetypeId",
                table: "cards",
                column: "ArchetypeId",
                principalTable: "Archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonsterCards_cards_CardId",
                table: "MonsterCards",
                column: "CardId",
                principalTable: "cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
