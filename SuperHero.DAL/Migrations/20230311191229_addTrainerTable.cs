using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addTrainerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsInfos_AspNetUsers_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "DoctorsInfos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DectorID",
                table: "DoctorsInfos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "TrainerInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Graduation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainerID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainerInfo_AspNetUsers_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos",
                column: "DectorID",
                unique: true,
                filter: "[DectorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerInfo_TrainerID",
                table: "TrainerInfo",
                column: "TrainerID",
                unique: true,
                filter: "[TrainerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsInfos_AspNetUsers_DectorID",
                table: "DoctorsInfos",
                column: "DectorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsInfos_AspNetUsers_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.DropTable(
                name: "TrainerInfo");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "DoctorsInfos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DectorID",
                table: "DoctorsInfos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos",
                column: "DectorID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsInfos_AspNetUsers_DectorID",
                table: "DoctorsInfos",
                column: "DectorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
