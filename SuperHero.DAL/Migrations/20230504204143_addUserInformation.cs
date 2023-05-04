using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addUserInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalysisPDF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    personID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Analyses_UserInfos_personID",
                        column: x => x.personID,
                        principalTable: "UserInfos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Radiologies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XRay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    personID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radiologies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Radiologies_UserInfos_personID",
                        column: x => x.personID,
                        principalTable: "UserInfos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    personID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Treatments_UserInfos_personID",
                        column: x => x.personID,
                        principalTable: "UserInfos",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_personID",
                table: "Analyses",
                column: "personID");

            migrationBuilder.CreateIndex(
                name: "IX_Radiologies_personID",
                table: "Radiologies",
                column: "personID");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_personID",
                table: "Treatments",
                column: "personID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "Radiologies");

            migrationBuilder.DropTable(
                name: "Treatments");
        }
    }
}
