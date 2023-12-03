using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleAndClaimRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Roles_RoleEntityId",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_RoleEntityId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "RoleEntityId",
                table: "Claims");

            migrationBuilder.CreateTable(
                name: "ClaimsEntityRoleEntity",
                columns: table => new
                {
                    ClaimsId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsEntityRoleEntity", x => new { x.ClaimsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ClaimsEntityRoleEntity_Claims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimsEntityRoleEntity_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimsEntityRoleEntity_RolesId",
                table: "ClaimsEntityRoleEntity",
                column: "RolesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimsEntityRoleEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleEntityId",
                table: "Claims",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_RoleEntityId",
                table: "Claims",
                column: "RoleEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Roles_RoleEntityId",
                table: "Claims",
                column: "RoleEntityId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
