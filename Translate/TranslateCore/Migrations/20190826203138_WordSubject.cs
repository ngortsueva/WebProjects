using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TranslateCore.Migrations
{
    public partial class WordSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Words",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WordTypeId",
                table: "Words",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WordSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSubjects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Words_SubjectId",
                table: "Words",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordTypeId",
                table: "Words",
                column: "WordTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordSubjects_SubjectId",
                table: "Words",
                column: "SubjectId",
                principalTable: "WordSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordTypes_WordTypeId",
                table: "Words",
                column: "WordTypeId",
                principalTable: "WordTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordSubjects_SubjectId",
                table: "Words");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordTypes_WordTypeId",
                table: "Words");

            migrationBuilder.DropTable(
                name: "WordSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Words_SubjectId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_WordTypeId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "WordTypeId",
                table: "Words");
        }
    }
}
