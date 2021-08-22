using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AmmendMedicalprescriptiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "MedicalPrescriptions");

            migrationBuilder.DropColumn(
                name: "medName",
                table: "MedicalPrescriptions");

            migrationBuilder.AddColumn<int>(
                name: "medId",
                table: "MedicalPrescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "medicineId",
                table: "MedicalPrescriptions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalPrescriptions_medicineId",
                table: "MedicalPrescriptions",
                column: "medicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalPrescriptions_Medicines_medicineId",
                table: "MedicalPrescriptions",
                column: "medicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalPrescriptions_Medicines_medicineId",
                table: "MedicalPrescriptions");

            migrationBuilder.DropIndex(
                name: "IX_MedicalPrescriptions_medicineId",
                table: "MedicalPrescriptions");

            migrationBuilder.DropColumn(
                name: "medId",
                table: "MedicalPrescriptions");

            migrationBuilder.DropColumn(
                name: "medicineId",
                table: "MedicalPrescriptions");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MedicalPrescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "medName",
                table: "MedicalPrescriptions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
