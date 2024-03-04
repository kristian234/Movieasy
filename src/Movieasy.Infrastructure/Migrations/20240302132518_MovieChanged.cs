using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movieasy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MovieChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_review_movies_movie_id",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "fk_review_user_user_id",
                table: "review");

            migrationBuilder.DropPrimaryKey(
                name: "pk_review",
                table: "review");

            migrationBuilder.RenameTable(
                name: "review",
                newName: "reviews");

            migrationBuilder.RenameIndex(
                name: "ix_review_user_id",
                table: "reviews",
                newName: "ix_reviews_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_review_movie_id",
                table: "reviews",
                newName: "ix_reviews_movie_id");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "release_date",
                table: "movies",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_reviews",
                table: "reviews",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_movies_movie_id",
                table: "reviews",
                column: "movie_id",
                principalTable: "movies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_reviews_users_user_id",
                table: "reviews",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reviews_movies_movie_id",
                table: "reviews");

            migrationBuilder.DropForeignKey(
                name: "fk_reviews_users_user_id",
                table: "reviews");

            migrationBuilder.DropPrimaryKey(
                name: "pk_reviews",
                table: "reviews");

            migrationBuilder.RenameTable(
                name: "reviews",
                newName: "review");

            migrationBuilder.RenameIndex(
                name: "ix_reviews_user_id",
                table: "review",
                newName: "ix_review_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_reviews_movie_id",
                table: "review",
                newName: "ix_review_movie_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "release_date",
                table: "movies",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_review",
                table: "review",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_review_movies_movie_id",
                table: "review",
                column: "movie_id",
                principalTable: "movies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_review_user_user_id",
                table: "review",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
