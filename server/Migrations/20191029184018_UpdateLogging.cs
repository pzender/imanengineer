using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TV_App.Migrations
{
    public partial class UpdateLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "posted",
                table: "GuideUpdates",
                newName: "started");

            migrationBuilder.RenameIndex(
                name: "IX_GuideUpdate_posted",
                table: "GuideUpdates",
                newName: "IX_GuideUpdate_started");

            migrationBuilder.AddColumn<DateTime>(
                name: "finished",
                table: "GuideUpdates",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuideUpdate_finished",
                table: "GuideUpdates",
                column: "finished",
                unique: true,
                filter: "[finished] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GuideUpdate_finished",
                table: "GuideUpdates");

            migrationBuilder.DropColumn(
                name: "finished",
                table: "GuideUpdates");

            migrationBuilder.RenameColumn(
                name: "started",
                table: "GuideUpdates",
                newName: "posted");

            migrationBuilder.RenameIndex(
                name: "IX_GuideUpdate_started",
                table: "GuideUpdates",
                newName: "IX_GuideUpdate_posted");
        }
    }
}
