using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class seedTImeslot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { "12:00 PM", "08:00 AM" });

            migrationBuilder.InsertData(
                table: "timeSlots",
                columns: new[] { "Id", "End", "Slot", "Start" },
                values: new object[] { 2, "07:00 PM", "Slot 2", "01:00 PM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { "12:00", "08:00" });
        }
    }
}
