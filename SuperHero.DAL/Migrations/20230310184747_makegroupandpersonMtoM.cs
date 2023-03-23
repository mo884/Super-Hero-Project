using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class makegroupandpersonMtoM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "PersonGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGroup", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonGroup_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonGroup_Groups_Group",
                        column: x => x.Group,
                        principalTable: "Groups",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroup_Group",
                table: "PersonGroup",
                column: "Group");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroup_PersonId",
                table: "PersonGroup",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonGroup");

            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupID",
                table: "AspNetUsers",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_GroupID",
                table: "AspNetUsers",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "ID");
        }
    }
}
