using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class Ammendedappointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "appointments",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "appointments",
                newName: "EndTime");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "appointments");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "appointments",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "appointments",
                newName: "EndDate");
        }
    }
}
