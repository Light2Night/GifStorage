using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GifStorage.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gifs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GifTag",
                columns: table => new
                {
                    GifsId = table.Column<long>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GifTag", x => new { x.GifsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_GifTag_Gifs_GifsId",
                        column: x => x.GifsId,
                        principalTable: "Gifs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GifTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GifTags",
                columns: table => new
                {
                    GifId = table.Column<long>(type: "INTEGER", nullable: false),
                    TagId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GifTags", x => new { x.GifId, x.TagId });
                    table.ForeignKey(
                        name: "FK_GifTags_Gifs_GifId",
                        column: x => x.GifId,
                        principalTable: "Gifs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GifTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GifTag_TagsId",
                table: "GifTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_GifTags_TagId",
                table: "GifTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GifTag");

            migrationBuilder.DropTable(
                name: "GifTags");

            migrationBuilder.DropTable(
                name: "Gifs");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
