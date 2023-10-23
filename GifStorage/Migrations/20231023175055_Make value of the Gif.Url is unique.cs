using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GifStorage.Migrations
{
    /// <inheritdoc />
    public partial class MakevalueoftheGifUrlisunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Gifs_Url",
                table: "Gifs",
                column: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Gifs_Url",
                table: "Gifs");
        }
    }
}
