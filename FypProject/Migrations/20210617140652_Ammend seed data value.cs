using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class Ammendseeddatavalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdBy", "createdOn" },
                values: new object[] { "$2a$11$XkmdECfiBNZtQC0yxvgtT.jioOGPC.QJHDoVQvvbfm5VD1GgiALj.", "Admin", "17/06/2021 10:06 PM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdBy", "createdOn" },
                values: new object[] { "$2a$11$3sMQo7m37MkyUnE7wdLoFOrSZSEpI5dqlK3RsNjIgPxOHjFQFNd9a", null, "17/06/2021 10:04 PM" });
        }
    }
}
