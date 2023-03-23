using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class vaddtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerInfo_AspNetUsers_TrainerID",
                table: "TrainerInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerInfo",
                table: "TrainerInfo");

            migrationBuilder.RenameTable(
                name: "TrainerInfo",
                newName: "TrainerInfos");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerInfo_TrainerID",
                table: "TrainerInfos",
                newName: "IX_TrainerInfos_TrainerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerInfos",
                table: "TrainerInfos",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerInfos_AspNetUsers_TrainerID",
                table: "TrainerInfos",
                column: "TrainerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerInfos_AspNetUsers_TrainerID",
                table: "TrainerInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerInfos",
                table: "TrainerInfos");

            migrationBuilder.RenameTable(
                name: "TrainerInfos",
                newName: "TrainerInfo");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerInfos_TrainerID",
                table: "TrainerInfo",
                newName: "IX_TrainerInfo_TrainerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerInfo",
                table: "TrainerInfo",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerInfo_AspNetUsers_TrainerID",
                table: "TrainerInfo",
                column: "TrainerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
