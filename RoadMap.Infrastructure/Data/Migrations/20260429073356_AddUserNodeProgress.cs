using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RoadMap.Infrastucture.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNodeProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserNodeProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    NodeId = table.Column<int>(type: "integer", nullable: false),
                    RoadmapId = table.Column<int>(type: "integer", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNodeProgresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNodeProgresses_UserId_RoadmapId_NodeId",
                table: "UserNodeProgresses",
                columns: new[] { "UserId", "RoadmapId", "NodeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNodeProgresses");
        }
    }
}
