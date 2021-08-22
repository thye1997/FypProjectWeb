using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class Seedoffdaytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OffDays",
                columns: new[] { "Id", "Day", "isOffDay" },
                values: new object[,]
                {
                    { 2, "Tuesday", false },
                    { 3, "Wednesday", false },
                    { 4, "Thursday", false },
                    { 5, "Friday", false },
                    { 6, "Saturday", false },
                    { 7, "Sunday", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OffDays",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OffDays",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OffDays",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OffDays",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OffDays",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OffDays",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
