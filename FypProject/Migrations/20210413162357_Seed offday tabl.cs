using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class Seedoffdaytabl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OffDays",
                columns: new[] { "Id", "Day", "isOffDay" },
                values: new object[] { 1, "Monday", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OffDays",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
