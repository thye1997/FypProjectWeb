using Microsoft.EntityFrameworkCore.Migrations;

namespace FypProject.Migrations
{
    public partial class Ammendaccprofileaccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentPushReminderEnabled",
                table: "AccountProfiles");

            migrationBuilder.DropColumn(
                name: "AppointmentSMSReminderEnabled",
                table: "AccountProfiles");

            migrationBuilder.DropColumn(
                name: "FirebaseToken",
                table: "AccountProfiles");

            migrationBuilder.DropColumn(
                name: "PushNotificationEnabled",
                table: "AccountProfiles");

            migrationBuilder.AddColumn<bool>(
                name: "AppointmentPushReminderEnabled",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AppointmentSMSReminderEnabled",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirebaseToken",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PushNotificationEnabled",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentPushReminderEnabled",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AppointmentSMSReminderEnabled",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FirebaseToken",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PushNotificationEnabled",
                table: "Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "AppointmentPushReminderEnabled",
                table: "AccountProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AppointmentSMSReminderEnabled",
                table: "AccountProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirebaseToken",
                table: "AccountProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PushNotificationEnabled",
                table: "AccountProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
