using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GLFManager.App.Migrations
{
    public partial class modifiedJobsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "Positions",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "PositionsAvailable",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Positions",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "PositionsAvailable",
                table: "Jobs");
        }
    }
}
