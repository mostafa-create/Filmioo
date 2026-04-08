using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class migr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genre_GenreId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Media_MediaId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie");

            migrationBuilder.RenameTable(
                name: "GenreMovie",
                newName: "GenreMedia");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMovie_GenreId",
                table: "GenreMedia",
                newName: "IX_GenreMedia_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMedia",
                table: "GenreMedia",
                columns: new[] { "MediaId", "GenreId" });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TV_Shows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfSeasons = table.Column<int>(type: "int", nullable: false),
                    IsOngoing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TV_Shows", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MediaId",
                table: "Movies",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMedia_Genre_GenreId",
                table: "GenreMedia",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMedia_Media_MediaId",
                table: "GenreMedia",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMedia_Genre_GenreId",
                table: "GenreMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMedia_Media_MediaId",
                table: "GenreMedia");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "TV_Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMedia",
                table: "GenreMedia");

            migrationBuilder.RenameTable(
                name: "GenreMedia",
                newName: "GenreMovie");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMedia_GenreId",
                table: "GenreMovie",
                newName: "IX_GenreMovie_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMovie",
                table: "GenreMovie",
                columns: new[] { "MediaId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genre_GenreId",
                table: "GenreMovie",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Media_MediaId",
                table: "GenreMovie",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
