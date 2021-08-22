using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class Ammendtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SystemUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "serviceType",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "doctorId",
                table: "appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "systemUserId",
                table: "appointments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$ElTIZmp.GYjzpzZdy9TyLO0hUzIfF.F4Qy8ceHbbtfaXxvLfw1Yx6", "03/07/2021 08:53 PM" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_SystemUsers_systemUserId",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_systemUserId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SystemUsers");

            migrationBuilder.DropColumn(
                name: "serviceType",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "doctorId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "systemUserId",
                table: "appointments");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$XkmdECfiBNZtQC0yxvgtT.jioOGPC.QJHDoVQvvbfm5VD1GgiALj.", "17/06/2021 10:06 PM" });
        }
    }
}
