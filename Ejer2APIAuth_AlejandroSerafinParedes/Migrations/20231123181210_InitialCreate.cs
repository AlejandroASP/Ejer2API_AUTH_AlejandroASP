using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ejer2API_AlejandroSerafinParedes.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    genreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.genreId);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    gameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.gameId);
                    table.ForeignKey(
                        name: "FK_Game_Genre_genreId",
                        column: x => x.genreId,
                        principalTable: "Genre",
                        principalColumn: "genreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "genreId", "genreName" },
                values: new object[,]
                {
                    { 1, "Lucha" },
                    { 2, "Acción" },
                    { 3, "Shooter" }
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "gameId", "gameName", "genreId" },
                values: new object[,]
                {
                    { 1, "El señor de los ajillos", 1 },
                    { 2, "Estreet Nocilla", 1 },
                    { 3, "Blancanieves y los 7 guanches", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_genreId",
                table: "Game",
                column: "genreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Genre");
        }
    }
}
