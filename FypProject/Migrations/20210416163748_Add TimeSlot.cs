using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AddTimeSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "timeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    End = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeSlots", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "timeSlots",
                columns: new[] { "Id", "End", "Slot", "Start" },
                values: new object[] { 1, "12:00", "Slot 1", "08:00" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "timeSlots");
        }
    }
}
