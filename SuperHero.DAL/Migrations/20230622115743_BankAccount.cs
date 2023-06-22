using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHero.DAL.Migrations
{
    /// <inheritdoc />
    public partial class BankAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardCVC = table.Column<int>(type: "int", nullable: false),
                    ExpMonth = table.Column<int>(type: "int", nullable: false),
                    ExpYear = table.Column<int>(type: "int", nullable: false),
                    EnterAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankAccounts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankAccounts");
        }
    }
}
