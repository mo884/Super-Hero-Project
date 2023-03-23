using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class relationbetweenreactandpost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ReactPosts_PostID",
                table: "ReactPosts",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReactPosts_Posts_PostID",
                table: "ReactPosts",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReactPosts_Posts_PostID",
                table: "ReactPosts");

            migrationBuilder.DropIndex(
                name: "IX_ReactPosts_PostID",
                table: "ReactPosts");
        }
    }
}
