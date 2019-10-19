using Microsoft.EntityFrameworkCore.Migrations;

namespace TranslateCore.Migrations
{
    public partial class WordCh2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordCategories_SubjectId",
                table: "Words");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Words",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Words_SubjectId",
                table: "Words",
                newName: "IX_Words_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordCategories_CategoryId",
                table: "Words",
                column: "CategoryId",
                principalTable: "WordCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordCategories_CategoryId",
                table: "Words");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Words",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Words_CategoryId",
                table: "Words",
                newName: "IX_Words_SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordCategories_SubjectId",
                table: "Words",
                column: "SubjectId",
                principalTable: "WordCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
