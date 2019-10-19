using Microsoft.EntityFrameworkCore.Migrations;

namespace TranslateCore.Migrations
{
    public partial class WordTypeCh3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "WordTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "WordTypes");
        }
    }
}
