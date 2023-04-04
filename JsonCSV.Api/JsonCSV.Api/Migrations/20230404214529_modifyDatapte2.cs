using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonCSV.Api.Migrations
{
    public partial class modifyDatapte2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "usersIdentification",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Role", "UserName" },
                values: new object[] { "Reader", "Raul" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "usersIdentification",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Role", "UserName" },
                values: new object[] { "Admin", "ww" });
        }
    }
}
