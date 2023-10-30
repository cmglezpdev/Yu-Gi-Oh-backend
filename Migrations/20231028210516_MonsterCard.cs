using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class MonsterCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Atk",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "Def",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "Rice",
                table: "cards");

            migrationBuilder.CreateTable(
                name: "MonsterCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Rice = table.Column<string>(type: "varchar(100)", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Atk = table.Column<int>(type: "integer", nullable: false),
                    Def = table.Column<int>(type: "integer", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonsterCards_cards_CardId",
                        column: x => x.CardId,
                        principalTable: "cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonsterCards_CardId",
                table: "MonsterCards",
                column: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonsterCards");

            migrationBuilder.AddColumn<int>(
                name: "Atk",
                table: "cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Def",
                table: "cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "cards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rice",
                table: "cards",
                type: "varchar(100)",
                nullable: true);
        }
    }
}
