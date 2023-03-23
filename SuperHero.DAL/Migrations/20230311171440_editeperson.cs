using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editeperson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos",
                column: "DectorID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos",
                column: "DectorID");
        }
    }
}
