using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AmmendRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_SystemUsers_systemUserId",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_systemUserId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "systemUserId",
                table: "appointments");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ybQDgPmiCWz76rqlf.ZmzOBSwsU3/qqG0rpHywmZqme5WEkFQfVSe");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_doctorId",
                table: "appointments",
                column: "doctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_SystemUsers_doctorId",
                table: "appointments",
                column: "doctorId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_SystemUsers_doctorId",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_doctorId",
                table: "appointments");

            migrationBuilder.AddColumn<int>(
                name: "systemUserId",
                table: "appointments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$0.EODnYTgaEWwYL74uoOxuk2fmfg/4Ss393UeKPlKolUIzS2oVxru");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_systemUserId",
                table: "appointments",
                column: "systemUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_SystemUsers_systemUserId",
                table: "appointments",
                column: "systemUserId",
                principalTable: "SystemUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
