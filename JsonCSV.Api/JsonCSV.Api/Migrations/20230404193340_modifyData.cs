using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonCSV.Api.Migrations
{
    public partial class modifyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "usersIdentification",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserName",
                value: "Alan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "usersIdentification",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserName",
                value: "Name");
        }
    }
}
