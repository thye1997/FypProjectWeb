using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class Ammendseeddataval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn", "userName" },
                values: new object[] { "$2a$11$3sMQo7m37MkyUnE7wdLoFOrSZSEpI5dqlK3RsNjIgPxOHjFQFNd9a", "17/06/2021 10:04 PM", "Doctor_1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn", "userName" },
                values: new object[] { "$2a$11$x0nkuixF8CLzDt8SWnWr8efeMk78nscmmV1rlaudytvQUdTett2XW", "15/06/2021 02:23 AM", "Doctor_2" });
        }
    }
}
