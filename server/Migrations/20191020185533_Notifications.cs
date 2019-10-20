using Microsoft.EntityFrameworkCore.Migrations;

namespace TV_App.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    user_login = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    emission_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false),
                    rating_value = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => new { x.user_login, x.emission_id });
                    table.UniqueConstraint("AK_Notifications_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notifications_Emissions_emission_id",
                        column: x => x.emission_id,
                        principalTable: "Emissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_user_login",
                        column: x => x.user_login,
                        principalTable: "Users",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Emission_id",
                table: "Notifications",
                column: "emission_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
