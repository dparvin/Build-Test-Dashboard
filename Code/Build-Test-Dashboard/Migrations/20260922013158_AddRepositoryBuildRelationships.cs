using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Build_Test_Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddRepositoryBuildRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TestRuns_BuildId",
                table: "TestRuns",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Builds_RepositoryId",
                table: "Builds",
                column: "RepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_Repositories_RepositoryId",
                table: "Builds",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestRuns_Builds_BuildId",
                table: "TestRuns",
                column: "BuildId",
                principalTable: "Builds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Builds_Repositories_RepositoryId",
                table: "Builds");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRuns_Builds_BuildId",
                table: "TestRuns");

            migrationBuilder.DropIndex(
                name: "IX_TestRuns_BuildId",
                table: "TestRuns");

            migrationBuilder.DropIndex(
                name: "IX_Builds_RepositoryId",
                table: "Builds");
        }
    }
}
