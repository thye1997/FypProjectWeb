using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$WoOMrdGXVoFTYOGNe.6HsuEmjD.HB9jwhfXfSxyVJmEItlpv.gla6", "17/08/2021 11:54 PM" });

            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "Slot",
                value: "Morning");

            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "Slot",
                value: "Afternoon");

            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "Slot",
                value: "Night");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$9Trmq9Ik4s9GKK8NwMzyaO2nnj9tZ8jb4hWT6lB9ZOAJeF8Rig5w6", "04/07/2021 01:54 AM" });

            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "Slot",
                value: "Slot 1");

            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "Slot",
                value: "Slot 2");

            migrationBuilder.UpdateData(
                table: "timeSlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "Slot",
                value: "Slot 3");
        }
    }
}
