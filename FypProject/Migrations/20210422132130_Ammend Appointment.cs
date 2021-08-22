using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AmmendAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Service",
                table: "appointments");

            migrationBuilder.AddColumn<int>(
                name: "serviceId",
                table: "appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_appointments_serviceId",
                table: "appointments",
                column: "serviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_Services_serviceId",
                table: "appointments",
                column: "serviceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_Services_serviceId",
                table: "appointments");

            migrationBuilder.DropIndex(
                name: "IX_appointments_serviceId",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "serviceId",
                table: "appointments");

            migrationBuilder.AddColumn<string>(
                name: "Service",
                table: "appointments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
