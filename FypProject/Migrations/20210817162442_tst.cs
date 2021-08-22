using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class tst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$o9nss5GPgtjqAUmwJp/9ruvGJ2A5QOA2tJ3iO4h.G.F43vwVOOBJe", "18/08/2021 12:24 AM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$nv1i3juibEqVWnSy7/hLM.4LLyOvi.1dkgcDcfomSe8wkveARbieS", "17/08/2021 11:59 PM" });
        }
    }
}
