using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addrecordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recorders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCheck = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recorders", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recorders");
        }
    }
}
