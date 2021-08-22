using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class AmmendtypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "serviceType",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "serviceTypeId",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "typeId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$9Trmq9Ik4s9GKK8NwMzyaO2nnj9tZ8jb4hWT6lB9ZOAJeF8Rig5w6", "04/07/2021 01:54 AM" });

            migrationBuilder.CreateIndex(
                name: "IX_Services_serviceTypeId",
                table: "Services",
                column: "serviceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceType_serviceTypeId",
                table: "Services",
                column: "serviceTypeId",
                principalTable: "ServiceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceType_serviceTypeId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_serviceTypeId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "serviceTypeId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "typeId",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "serviceType",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SystemUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "createdOn" },
                values: new object[] { "$2a$11$qdYxJYnrUegPJLcX6kPxku6H/KirzeFc4oYpwsKz/LF7Q.B2feGEW", "04/07/2021 01:44 AM" });
        }
    }
}
