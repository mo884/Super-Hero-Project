using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditeCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses",
                column: "CategoryID",
                principalTable: "Catogeries",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses",
                column: "CategoryID",
                principalTable: "Catogeries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
