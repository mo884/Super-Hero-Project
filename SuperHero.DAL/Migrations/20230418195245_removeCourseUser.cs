using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeCourseUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesComment_Courses_courseId",
                table: "CoursesComment");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursesComment",
                table: "CoursesComment");

            migrationBuilder.RenameTable(
                name: "CoursesComment",
                newName: "coursesComments");

            migrationBuilder.RenameIndex(
                name: "IX_CoursesComment_courseId",
                table: "coursesComments",
                newName: "IX_coursesComments_courseId");

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_coursesComments",
                table: "coursesComments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PersonId",
                table: "Courses",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_PersonId",
                table: "Courses",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_coursesComments_Courses_courseId",
                table: "coursesComments",
                column: "courseId",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_PersonId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_coursesComments_Courses_courseId",
                table: "coursesComments");

            migrationBuilder.DropIndex(
                name: "IX_Courses_PersonId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_coursesComments",
                table: "coursesComments");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "coursesComments",
                newName: "CoursesComment");

            migrationBuilder.RenameIndex(
                name: "IX_coursesComments_courseId",
                table: "CoursesComment",
                newName: "IX_CoursesComment_courseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursesComment",
                table: "CoursesComment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersonType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserCourses_AspNetUsers_PersonID",
                        column: x => x.PersonID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseID",
                table: "UserCourses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_PersonID",
                table: "UserCourses",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesComment_Courses_courseId",
                table: "CoursesComment",
                column: "courseId",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
