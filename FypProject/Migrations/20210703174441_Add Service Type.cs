using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AddServiceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ServiceType",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Medical Test" },
                    { 2, "Vaccination" },
                    { 3, "Other" }
                });

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$qdYxJYnrUegPJLcX6kPxku6H/KirzeFc4oYpwsKz/LF7Q.B2feGEW", "04/07/2021 01:44 AM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$ElTIZmp.GYjzpzZdy9TyLO0hUzIfF.F4Qy8ceHbbtfaXxvLfw1Yx6", "03/07/2021 08:53 PM" });
        }
    }
}
