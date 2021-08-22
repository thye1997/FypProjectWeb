using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$nv1i3juibEqVWnSy7/hLM.4LLyOvi.1dkgcDcfomSe8wkveARbieS", "17/08/2021 11:59 PM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$WoOMrdGXVoFTYOGNe.6HsuEmjD.HB9jwhfXfSxyVJmEItlpv.gla6", "17/08/2021 11:54 PM" });

            migrationBuilder.DropTable(
               name: "MedicalPrescriptions");
        }
    }
}
