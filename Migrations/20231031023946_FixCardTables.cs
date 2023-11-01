using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixCardTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_monsters_cards_CardId",
                table: "monsters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_monsters",
                table: "monsters");

            migrationBuilder.RenameTable(
                name: "monsters",
                newName: "monster_cards");

            migrationBuilder.RenameIndex(
                name: "IX_monsters_CardId",
                table: "monster_cards",
                newName: "IX_monster_cards_CardId");

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                table: "cards",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "monster_cards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Def",
                table: "monster_cards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Atk",
                table: "monster_cards",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_monster_cards",
                table: "monster_cards",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "spell_cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spell_cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_spell_cards_cards_CardId",
                        column: x => x.CardId,
                        principalTable: "cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trap_cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trap_cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trap_cards_cards_CardId",
                        column: x => x.CardId,
                        principalTable: "cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_spell_cards_CardId",
                table: "spell_cards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_trap_cards_CardId",
                table: "trap_cards",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_monster_cards_cards_CardId",
                table: "monster_cards",
                column: "CardId",
                principalTable: "cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_monster_cards_cards_CardId",
                table: "monster_cards");

            migrationBuilder.DropTable(
                name: "spell_cards");

            migrationBuilder.DropTable(
                name: "trap_cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_monster_cards",
                table: "monster_cards");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "cards");

            migrationBuilder.RenameTable(
                name: "monster_cards",
                newName: "monsters");

            migrationBuilder.RenameIndex(
                name: "IX_monster_cards_CardId",
                table: "monsters",
                newName: "IX_monsters_CardId");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Def",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Atk",
                table: "monsters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_monsters",
                table: "monsters",
                column: "Id");

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
