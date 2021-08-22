using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class adddurationdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "slotDurations",
                columns: new[] { "Id", "isActive", "slotDuration" },
                values: new object[] { 1, false, 15 });

            migrationBuilder.InsertData(
                table: "slotDurations",
                columns: new[] { "Id", "isActive", "slotDuration" },
                values: new object[] { 2, true, 30 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "slotDurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "slotDurations",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
