using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadMap.Infrastucture.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixUserIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"UserNodeProgresses\" ALTER COLUMN \"UserId\" TYPE integer USING \"UserId\"::integer");

            migrationBuilder.CreateIndex(
                name: "IX_UserNodeProgresses_NodeId",
                table: "UserNodeProgresses",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNodeProgresses_RoadmapId",
                table: "UserNodeProgresses",
                column: "RoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNodeProgresses_UserId",
                table: "UserNodeProgresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNodeProgresses_Nodes_NodeId",
                table: "UserNodeProgresses",
                column: "NodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNodeProgresses_Roadmaps_RoadmapId",
                table: "UserNodeProgresses",
                column: "RoadmapId",
                principalTable: "Roadmaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserNodeProgresses_Users_UserId",
                table: "UserNodeProgresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNodeProgresses_Nodes_NodeId",
                table: "UserNodeProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNodeProgresses_Roadmaps_RoadmapId",
                table: "UserNodeProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNodeProgresses_Users_UserId",
                table: "UserNodeProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserNodeProgresses_NodeId",
                table: "UserNodeProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserNodeProgresses_RoadmapId",
                table: "UserNodeProgresses");

            migrationBuilder.DropIndex(
                name: "IX_UserNodeProgresses_UserId",
                table: "UserNodeProgresses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserNodeProgresses",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
