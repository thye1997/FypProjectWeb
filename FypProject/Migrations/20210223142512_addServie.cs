using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class addServie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdOn",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "createdOn",
                table: "Services");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
