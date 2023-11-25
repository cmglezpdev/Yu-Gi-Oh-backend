using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeApproveInInscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "tournament_inscriptions");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "tournament_inscriptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tournament_inscriptions");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "tournament_inscriptions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
