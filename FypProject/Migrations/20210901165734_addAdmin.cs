using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Role", "userName" },
                values: new object[] { "$2a$11$z0PEoZWNwkvAi/oDKo6ENe6qAIGYOmGjGGs61g71I384/szOPiG6u", "Admin", "Admin_1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Role", "userName" },
                values: new object[] { "$2a$11$ybQDgPmiCWz76rqlf.ZmzOBSwsU3/qqG0rpHywmZqme5WEkFQfVSe", "Doctor", "Doctor_1" });
        }
    }
}
