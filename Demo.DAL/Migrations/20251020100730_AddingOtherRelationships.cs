using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddingOtherRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MoviesPersons_PersonId",
                table: "MoviesPersons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_GenreId",
                table: "GenreMovie",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genre_GenreId",
                table: "GenreMovie",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movies_MovieId",
                table: "GenreMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesPersons_Movies_MovieId",
                table: "MoviesPersons",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesPersons_Persons_PersonId",
                table: "MoviesPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genre_GenreId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movies_MovieId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesPersons_Movies_MovieId",
                table: "MoviesPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesPersons_Persons_PersonId",
                table: "MoviesPersons");

            migrationBuilder.DropIndex(
                name: "IX_MoviesPersons_PersonId",
                table: "MoviesPersons");

            migrationBuilder.DropIndex(
                name: "IX_GenreMovie_GenreId",
                table: "GenreMovie");
        }
    }
}
