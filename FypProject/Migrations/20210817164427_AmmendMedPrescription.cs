using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AmmendMedPrescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userId",
                table: "MedicalPrescription",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MedicalPrescription",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$0.EODnYTgaEWwYL74uoOxuk2fmfg/4Ss393UeKPlKolUIzS2oVxru", "18/08/2021 12:24 AM" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalPrescription_UserId",
                table: "MedicalPrescription",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalPrescription_Users_UserId",
                table: "MedicalPrescription",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalPrescription_Users_UserId",
                table: "MedicalPrescription");

            migrationBuilder.DropIndex(
                name: "IX_MedicalPrescription_UserId",
                table: "MedicalPrescription");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MedicalPrescription",
                newName: "userId");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "MedicalPrescription",
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
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$8MsM.t39Y3JhbeXUUWG1ru2TAhnzitW2f1S729h3sgGVujkHcOgsW", "18/08/2021 12:36 AM" });
        }
    }
}
