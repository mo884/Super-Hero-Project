using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsInfos_AspNetUsers_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroup_AspNetUsers_PersonId",
                table: "PersonGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroup_Groups_Group",
                table: "PersonGroup");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_DonnerInfos_DonnerID",
                table: "DonnerInfos");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonGroup",
                table: "PersonGroup");

            migrationBuilder.DropColumn(
                name: "Sticker",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "PersonGroup",
                newName: "personGroups");

            migrationBuilder.RenameIndex(
                name: "IX_PersonGroup_PersonId",
                table: "personGroups",
                newName: "IX_personGroups_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonGroup_Group",
                table: "personGroups",
                newName: "IX_personGroups_Group");

            migrationBuilder.AlterColumn<string>(
                name: "PersonID",
                table: "ReactPosts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Like",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "video",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Lessons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<string>(
                name: "CV",
                table: "DoctorsInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_personGroups",
                table: "personGroups",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    FriendId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    personId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsFriend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => new { x.personId, x.FriendId });
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerInfos",
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
                    table.PrimaryKey("PK_TrainerInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrainerInfos_AspNetUsers_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReactPosts_PostID",
                table: "ReactPosts",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_DonnerInfos_DonnerID",
                table: "DonnerInfos",
                column: "DonnerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos",
                column: "DectorID",
                unique: true,
                filter: "[DectorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendId",
                table: "Friends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerInfos_TrainerID",
                table: "TrainerInfos",
                column: "TrainerID",
                unique: true,
                filter: "[TrainerID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses",
                column: "CategoryID",
                principalTable: "Catogeries",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsInfos_AspNetUsers_DectorID",
                table: "DoctorsInfos",
                column: "DectorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseID",
                table: "Lessons",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_personGroups_AspNetUsers_PersonId",
                table: "personGroups",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personGroups_Groups_Group",
                table: "personGroups",
                column: "Group",
                principalTable: "Groups",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReactPosts_Posts_PostID",
                table: "ReactPosts",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsInfos_AspNetUsers_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_personGroups_AspNetUsers_PersonId",
                table: "personGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_personGroups_Groups_Group",
                table: "personGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ReactPosts_Posts_PostID",
                table: "ReactPosts");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "TrainerInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_ReactPosts_PostID",
                table: "ReactPosts");

            migrationBuilder.DropIndex(
                name: "IX_DonnerInfos_DonnerID",
                table: "DonnerInfos");

            migrationBuilder.DropIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personGroups",
                table: "personGroups");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "personGroups",
                newName: "PersonGroup");

            migrationBuilder.RenameIndex(
                name: "IX_personGroups_PersonId",
                table: "PersonGroup",
                newName: "IX_PersonGroup_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_personGroups_Group",
                table: "PersonGroup",
                newName: "IX_PersonGroup_Group");

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "ReactPosts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "video",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "CV",
                table: "DoctorsInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sticker",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonGroup",
                table: "PersonGroup",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserID",
                table: "UserInfos",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DonnerInfos_DonnerID",
                table: "DonnerInfos",
                column: "DonnerID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsInfos_DectorID",
                table: "DoctorsInfos",
                column: "DectorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Catogeries_CategoryID",
                table: "Courses",
                column: "CategoryID",
                principalTable: "Catogeries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsInfos_AspNetUsers_DectorID",
                table: "DoctorsInfos",
                column: "DectorID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseID",
                table: "Lessons",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGroup_AspNetUsers_PersonId",
                table: "PersonGroup",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonGroup_Groups_Group",
                table: "PersonGroup",
                column: "Group",
                principalTable: "Groups",
                principalColumn: "ID");
        }
    }
}
