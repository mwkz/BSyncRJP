using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BS.Transactions.Infrastructure.Migrations
{
    public partial class AddAccountBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountBalance",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBalance", x => x.AccountId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransaction_AccountBalance_AccountId",
                table: "AccountTransaction",
                column: "AccountId",
                principalTable: "AccountBalance",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransaction_AccountBalance_AccountId",
                table: "AccountTransaction");

            migrationBuilder.DropTable(
                name: "AccountBalance");
        }
    }
}
