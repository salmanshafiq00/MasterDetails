using Microsoft.EntityFrameworkCore.Migrations;

namespace TestConfiguration.Migrations
{
    public partial class addProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileUrl",
                table: "TestConfigs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatureUrl",
                table: "TestConfigs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileUrl",
                table: "TestConfigs");

            migrationBuilder.DropColumn(
                name: "SignatureUrl",
                table: "TestConfigs");
        }
    }
}
