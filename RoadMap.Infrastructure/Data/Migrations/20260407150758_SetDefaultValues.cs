using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadMap.Infrastucture.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoadmapNodeNodeId",
                table: "NodeDependencies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoadmapNodeRoadmapId",
                table: "NodeDependencies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NodeDependencies_RoadmapNodeRoadmapId_RoadmapNodeNodeId",
                table: "NodeDependencies",
                columns: new[] { "RoadmapNodeRoadmapId", "RoadmapNodeNodeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NodeDependencies_RoadmapNodes_RoadmapNodeRoadmapId_RoadmapN~",
                table: "NodeDependencies",
                columns: new[] { "RoadmapNodeRoadmapId", "RoadmapNodeNodeId" },
                principalTable: "RoadmapNodes",
                principalColumns: new[] { "RoadmapId", "NodeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NodeDependencies_RoadmapNodes_RoadmapNodeRoadmapId_RoadmapN~",
                table: "NodeDependencies");

            migrationBuilder.DropIndex(
                name: "IX_NodeDependencies_RoadmapNodeRoadmapId_RoadmapNodeNodeId",
                table: "NodeDependencies");

            migrationBuilder.DropColumn(
                name: "RoadmapNodeNodeId",
                table: "NodeDependencies");

            migrationBuilder.DropColumn(
                name: "RoadmapNodeRoadmapId",
                table: "NodeDependencies");
        }
    }
}
