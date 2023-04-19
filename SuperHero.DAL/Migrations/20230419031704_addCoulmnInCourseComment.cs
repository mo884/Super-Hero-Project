using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addCoulmnInCourseComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "coursesComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_coursesComments_UserId",
                table: "coursesComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_coursesComments_AspNetUsers_UserId",
                table: "coursesComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coursesComments_AspNetUsers_UserId",
                table: "coursesComments");

            migrationBuilder.DropIndex(
                name: "IX_coursesComments_UserId",
                table: "coursesComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "coursesComments");
        }
    }
}
