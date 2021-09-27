using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class optionalFKId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_SystemUsers_doctorId",
                table: "appointments");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$LG8/09QAi5QUbl6msFxJu.gQ.cql5ex.09aJXPyLc4TiDARYn23F.");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_SystemUsers_doctorId",
                table: "appointments",
                column: "doctorId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_SystemUsers_doctorId",
                table: "appointments");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$z0PEoZWNwkvAi/oDKo6ENe6qAIGYOmGjGGs61g71I384/szOPiG6u");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_SystemUsers_doctorId",
                table: "appointments",
                column: "doctorId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
