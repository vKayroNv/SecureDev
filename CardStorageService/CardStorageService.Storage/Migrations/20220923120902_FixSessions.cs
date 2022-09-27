using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardStorageService.Storage.Migrations
{
    public partial class FixSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "AccountSessions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "AccountSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
