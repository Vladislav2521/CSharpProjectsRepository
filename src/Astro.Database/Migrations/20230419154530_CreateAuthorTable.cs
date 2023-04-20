using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Astro.Database.Migrations
{
    public partial class CreateAuthorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "author",
                table: "book");

            migrationBuilder.AddColumn<int>(
                name: "author_id",
                table: "book",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_author", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_book_author_id",
                table: "book",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "fk_book_author_author_id",
                table: "book",
                column: "author_id",
                principalTable: "author",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_book_author_author_id",
                table: "book");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropIndex(
                name: "ix_book_author_id",
                table: "book");

            migrationBuilder.DropColumn(
                name: "author_id",
                table: "book");

            migrationBuilder.AddColumn<string>(
                name: "author",
                table: "book",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
