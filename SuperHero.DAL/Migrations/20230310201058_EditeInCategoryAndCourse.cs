using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EditeInCategoryAndCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catogeries_Courses_CourseID",
                table: "Catogeries");

            migrationBuilder.DropIndex(
                name: "IX_Catogeries_CourseID",
                table: "Catogeries");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Catogeries");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Catogeries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Catogeries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryID",
                table: "Courses",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses",
                column: "CategoryID",
                principalTable: "Catogeries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CategoryID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Catogeries");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Catogeries");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Catogeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Catogeries_CourseID",
                table: "Catogeries",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Catogeries_Courses_CourseID",
                table: "Catogeries",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
