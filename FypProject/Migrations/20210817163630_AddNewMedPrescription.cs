using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AddNewMedPrescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalPrescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    apptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalPrescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalPrescription_appointments_apptId",
                        column: x => x.apptId,
                        principalTable: "appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalPrescription_Medicines_medId",
                        column: x => x.medId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$8MsM.t39Y3JhbeXUUWG1ru2TAhnzitW2f1S729h3sgGVujkHcOgsW", "18/08/2021 12:36 AM" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalPrescription_apptId",
                table: "MedicalPrescription",
                column: "apptId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalPrescription_medId",
                table: "MedicalPrescription",
                column: "medId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalPrescription");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$o9nss5GPgtjqAUmwJp/9ruvGJ2A5QOA2tJ3iO4h.G.F43vwVOOBJe", "18/08/2021 12:24 AM" });
        }
    }
}
