using Microsoft.EntityFrameworkCore.Migrations;

namespace TranslateCore.Migrations
{
    public partial class WordType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeRu",
                table: "WordTypes",
                newName: "NameRu");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "WordTypes",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameRu",
                table: "WordTypes",
                newName: "TypeRu");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "WordTypes",
                newName: "Type");
        }
    }
}
