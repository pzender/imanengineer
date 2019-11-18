using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace TV_App.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class PushNotificationsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "push_auth",
                table: "Users",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "push_endpoint",
                table: "Users",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "push_p256dh",
                table: "Users",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "push_auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "push_endpoint",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "push_p256dh",
                table: "Users");
        }
    }
}
