using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BS.Accounts.Infrastructure.Migrations
{
    public partial class AddMissingAccountFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNo",
                table: "Account",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountNo",
                table: "Account",
                column: "AccountNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Account_AccountNo",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Account");
        }
    }
}
