using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AmmendOptFKID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "doctorId",
                table: "appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$R3Th0j8lSBW4Mh0sM8WmhOl1fB/0svq/lC8qkgdHmkaMfEkno50Dm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "doctorId",
                table: "appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$LG8/09QAi5QUbl6msFxJu.gQ.cql5ex.09aJXPyLc4TiDARYn23F.");
        }
    }
}
