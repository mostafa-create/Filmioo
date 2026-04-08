using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class migr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "TV_Shows");

            migrationBuilder.AddColumn<bool>(
                name: "IsMovie",
                table: "Media",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOngoing",
                table: "Media",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeasons",
                table: "Media",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMovie",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "IsOngoing",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "NumberOfSeasons",
                table: "Media");

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
                    IsOngoing = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfSeasons = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TV_Shows", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MediaId",
                table: "Movies",
                column: "MediaId");
        }
    }
}
