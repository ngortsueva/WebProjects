using Microsoft.EntityFrameworkCore.Migrations;

namespace TranslateCore.Migrations
{
    public partial class WordCategoryCh2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "WordCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "WordCategories");
        }
    }
}
