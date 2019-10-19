using Microsoft.EntityFrameworkCore.Migrations;

namespace CountryWeb.Migrations
{
    public partial class Address2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flat",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flat",
                table: "Addresses");
        }
    }
}
