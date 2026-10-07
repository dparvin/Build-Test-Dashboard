using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Build_Test_Dashboard.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Configuration = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildConnections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Project = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RepositoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Builds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildConnectionId = table.Column<int>(type: "int", nullable: false),
                    SourceRepositoryId = table.Column<int>(type: "int", nullable: true),
                    SourceProvider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SourceOwner = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SourceProject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SourceRepositoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExternalBuildId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BuildNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Commit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Started = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Completed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Builds_BuildConnections_BuildConnectionId",
                        column: x => x.BuildConnectionId,
                        principalTable: "BuildConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Builds_Repositories_SourceRepositoryId",
                        column: x => x.SourceRepositoryId,
                        principalTable: "Repositories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestRuns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Passed = table.Column<int>(type: "int", nullable: false),
                    Failed = table.Column<int>(type: "int", nullable: false),
                    Skipped = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestRuns_Builds_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Builds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildConnections_Provider_Name",
                table: "BuildConnections",
                columns: new[] { "Provider", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Builds_BuildConnectionId_ExternalBuildId",
                table: "Builds",
                columns: new[] { "BuildConnectionId", "ExternalBuildId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Builds_SourceRepositoryId",
                table: "Builds",
                column: "SourceRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRuns_BuildId",
                table: "TestRuns",
                column: "BuildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestRuns");

            migrationBuilder.DropTable(
                name: "Builds");

            migrationBuilder.DropTable(
                name: "BuildConnections");

            migrationBuilder.DropTable(
                name: "Repositories");
        }
    }
}
