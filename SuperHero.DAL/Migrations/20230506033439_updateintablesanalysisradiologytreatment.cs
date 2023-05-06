using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateintablesanalysisradiologytreatment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorID",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdd",
                table: "Treatments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorID",
                table: "Radiologies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdd",
                table: "Radiologies",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorID",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdd",
                table: "Analyses",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "IsAdd",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Radiologies");

            migrationBuilder.DropColumn(
                name: "IsAdd",
                table: "Radiologies");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Analyses");

            migrationBuilder.DropColumn(
                name: "IsAdd",
                table: "Analyses");
        }
    }
}
