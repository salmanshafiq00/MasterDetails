using Microsoft.EntityFrameworkCore.Migrations;

namespace TestConfiguration.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionReferenceId = table.Column<int>(type: "int", nullable: false),
                    BatchId = table.Column<int>(type: "int", nullable: false),
                    SesionId = table.Column<int>(type: "int", nullable: false),
                    MinimumGPA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PassMark = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestConfigDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestConfigId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    ResultGradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestConfigDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestConfigDetails_TestConfigs_TestConfigId",
                        column: x => x.TestConfigId,
                        principalTable: "TestConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestConfigDetails_TestConfigId",
                table: "TestConfigDetails",
                column: "TestConfigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestConfigDetails");

            migrationBuilder.DropTable(
                name: "TestConfigs");
        }
    }
}
