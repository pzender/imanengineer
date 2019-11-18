using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace TV_App.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class SeparateSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user_login = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    push_endpoint = table.Column<string>(maxLength: 250, nullable: true),
                    push_p256dh = table.Column<string>(maxLength: 250, nullable: true),
                    push_auth = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_user_login",
                        column: x => x.user_login,
                        principalTable: "Users",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_user_login",
                table: "Subscriptions",
                column: "user_login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "push_auth",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "push_endpoint",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "push_p256dh",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
