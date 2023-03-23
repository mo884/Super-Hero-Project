using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroup_AspNetUsers_PersonId",
                table: "PersonGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonGroup_Groups_Group",
                table: "PersonGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonGroup",
                table: "PersonGroup");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_personGroups",
                table: "personGroups",
                column: "ID");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personGroups_AspNetUsers_PersonId",
                table: "personGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_personGroups_Groups_Group",
                table: "personGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personGroups",
                table: "personGroups");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonGroup",
                table: "PersonGroup",
                column: "ID");

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
