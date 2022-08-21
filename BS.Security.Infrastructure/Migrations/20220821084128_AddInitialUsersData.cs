using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BS.Security.Infrastructure.Migrations
{
    public partial class AddInitialUsersData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedDate", "Enabled", "Password", "UpdatedDate", "Username" },
                values: new object[] { 1, new DateTime(2022, 8, 21, 8, 41, 28, 359, DateTimeKind.Utc).AddTicks(9687), true, "admin", new DateTime(2022, 8, 21, 8, 41, 28, 359, DateTimeKind.Utc).AddTicks(9688), "admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedDate", "Enabled", "Password", "UpdatedDate", "Username" },
                values: new object[] { 2, new DateTime(2022, 8, 21, 8, 41, 28, 359, DateTimeKind.Utc).AddTicks(9703), true, "user", new DateTime(2022, 8, 21, 8, 41, 28, 359, DateTimeKind.Utc).AddTicks(9703), "user" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
