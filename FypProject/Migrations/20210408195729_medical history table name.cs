using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class medicalhistorytablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistory_Users_userId",
                table: "MedicalHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalHistory",
                table: "MedicalHistory");

            migrationBuilder.RenameTable(
                name: "MedicalHistory",
                newName: "MedicalHistorys");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalHistory_userId",
                table: "MedicalHistorys",
                newName: "IX_MedicalHistorys_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalHistorys",
                table: "MedicalHistorys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistorys_Users_userId",
                table: "MedicalHistorys",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalHistorys_Users_userId",
                table: "MedicalHistorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalHistorys",
                table: "MedicalHistorys");

            migrationBuilder.RenameTable(
                name: "MedicalHistorys",
                newName: "MedicalHistory");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalHistorys_userId",
                table: "MedicalHistory",
                newName: "IX_MedicalHistory_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalHistory",
                table: "MedicalHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalHistory_Users_userId",
                table: "MedicalHistory",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
