using Microsoft.EntityFrameworkCore.Migrations;

namespace TV_App.Migrations
{
    public partial class ChannelGrouping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Offers",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "Offers");
        }
    }
}
