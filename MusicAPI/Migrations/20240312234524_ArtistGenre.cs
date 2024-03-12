using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicAPI.Migrations
{
    /// <inheritdoc />
    public partial class ArtistGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistGenre_Artists_ArtistId",
                table: "ArtistGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistGenre_Genres_GenreId",
                table: "ArtistGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistGenre",
                table: "ArtistGenre");

            migrationBuilder.RenameTable(
                name: "ArtistGenre",
                newName: "ArtistGenres");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistGenre_GenreId",
                table: "ArtistGenres",
                newName: "IX_ArtistGenres_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistGenres",
                table: "ArtistGenres",
                columns: new[] { "ArtistId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistGenres_Artists_ArtistId",
                table: "ArtistGenres",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistGenres_Genres_GenreId",
                table: "ArtistGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistGenres_Artists_ArtistId",
                table: "ArtistGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistGenres_Genres_GenreId",
                table: "ArtistGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistGenres",
                table: "ArtistGenres");

            migrationBuilder.RenameTable(
                name: "ArtistGenres",
                newName: "ArtistGenre");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistGenres_GenreId",
                table: "ArtistGenre",
                newName: "IX_ArtistGenre_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistGenre",
                table: "ArtistGenre",
                columns: new[] { "ArtistId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistGenre_Artists_ArtistId",
                table: "ArtistGenre",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistGenre_Genres_GenreId",
                table: "ArtistGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
