using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class SeedSlotlast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "Start",
                value: "12:00 PM");

            migrationBuilder.InsertData(
                table: "timeSlots",
                columns: new[] { "Id", "End", "Slot", "Start" },
                values: new object[] { 3, "10:00 PM", "Slot 3", "07:00 PM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "Start",
                value: "01:00 PM");
        }
    }
}
