using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addDonnerforign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DonnerInfos_DonnerID",
                table: "DonnerInfos");

            migrationBuilder.AlterColumn<string>(
                name: "CV",
                table: "DoctorsInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DonnerInfos_DonnerID",
                table: "DonnerInfos",
                column: "DonnerID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DonnerInfos_DonnerID",
                table: "DonnerInfos");

            migrationBuilder.AlterColumn<string>(
                name: "CV",
                table: "DoctorsInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonnerInfos_DonnerID",
                table: "DonnerInfos",
                column: "DonnerID");
        }
    }
}
