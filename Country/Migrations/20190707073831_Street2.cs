using Microsoft.EntityFrameworkCore.Migrations;

namespace CountryWeb.Migrations
{
    public partial class Street2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Buildings",
                table: "Streets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buildings",
                table: "Streets");
        }
    }
}
