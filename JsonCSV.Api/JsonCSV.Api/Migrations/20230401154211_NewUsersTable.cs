using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JsonCSV.Api.Migrations
{
    public partial class NewUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usersIdentification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersIdentification", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "usersIdentification",
                columns: new[] { "Id", "PasswordHash", "Role", "UserName" },
                values: new object[] { 1, "WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=", "Admin", "Name" });

            migrationBuilder.InsertData(
                table: "usersIdentification",
                columns: new[] { "Id", "PasswordHash", "Role", "UserName" },
                values: new object[] { 2, "uGwaNkDuybwje2YFH52Iug8hzhdsw14OqkLN0HLjgxY=", "Admin", "ww" });

            migrationBuilder.InsertData(
                table: "usersIdentification",
                columns: new[] { "Id", "PasswordHash", "Role", "UserName" },
                values: new object[] { 3, "iycDVeU89JiQiiTck7PC9Wx2W18ocSwK2GxPPkuCSsU=", "Admin", "wwww" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usersIdentification");
        }
    }
}
