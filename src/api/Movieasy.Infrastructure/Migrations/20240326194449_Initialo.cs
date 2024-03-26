using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movieasy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genre_movie",
                columns: table => new
                {
                    genres_id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_genre_movie", x => new { x.genres_id, x.movie_id });
                    table.ForeignKey(
                        name: "fk_genre_movie_genres_genres_id",
                        column: x => x.genres_id,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_genre_movie_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_genre_movie_movie_id",
                table: "genre_movie",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "ix_genres_name",
                table: "genres",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "genre_movie");

            migrationBuilder.DropTable(
                name: "genres");
        }
    }
}
