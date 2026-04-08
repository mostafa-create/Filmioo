using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movies_MovieId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_MovieId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "MoviesPersons");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "GenreMovie");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Reviews",
                newName: "MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                newName: "IX_Reviews_MediaId");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "GenreMovie",
                newName: "MediaId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Score",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie",
                columns: new[] { "MediaId", "GenreId" });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Release_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateAddedToDB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Runtime = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Media_MediaId",
                table: "GenreMovie",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Media_MediaId",
                table: "Reviews",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Media_MediaId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Media_MediaId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "Reviews",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_MediaId",
                table: "Reviews",
                newName: "IX_Reviews_MovieId");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "GenreMovie",
                newName: "MovieId");

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "GenreMovie",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie",
                columns: new[] { "MovieId", "GenreId", "Role" });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Movie_Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Release_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Runtime = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "MoviesPersons",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesPersons", x => new { x.MovieId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_MoviesPersons_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesPersons_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesPersons_PersonId",
                table: "MoviesPersons",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movies_MovieId",
                table: "GenreMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_MovieId",
                table: "Reviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
