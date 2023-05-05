using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNameAnalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Radiologies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Radiologies");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Analyses");
        }
    }
}
