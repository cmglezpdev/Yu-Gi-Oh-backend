using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class NulleableArchetypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_archetypes_ArchetypeId",
                table: "cards");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArchetypeId",
                table: "cards",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_archetypes_ArchetypeId",
                table: "cards",
                column: "ArchetypeId",
                principalTable: "archetypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_archetypes_ArchetypeId",
                table: "cards");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArchetypeId",
                table: "cards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_cards_archetypes_ArchetypeId",
                table: "cards",
                column: "ArchetypeId",
                principalTable: "archetypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
