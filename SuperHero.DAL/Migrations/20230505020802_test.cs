using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PatientID",
                table: "Recorders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Recorders_PatientID",
                table: "Recorders",
                column: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recorders_AspNetUsers_PatientID",
                table: "Recorders",
                column: "PatientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recorders_AspNetUsers_PatientID",
                table: "Recorders");

            migrationBuilder.DropIndex(
                name: "IX_Recorders_PatientID",
                table: "Recorders");

            migrationBuilder.AlterColumn<string>(
                name: "PatientID",
                table: "Recorders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
