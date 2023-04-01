using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonCSV.Api.Migrations
{
    public partial class addingnameproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "usersIdentification",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "usersIdentification",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "asddd");

            migrationBuilder.UpdateData(
                table: "usersIdentification",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "asddd");

            migrationBuilder.UpdateData(
                table: "usersIdentification",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "asddd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "usersIdentification");
        }
    }
}
