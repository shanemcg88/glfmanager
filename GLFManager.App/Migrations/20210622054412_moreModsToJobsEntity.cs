using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GLFManager.App.Migrations
{
    public partial class moreModsToJobsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionsAvailable",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPositions",
                table: "Jobs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPositions",
                table: "Jobs");

            migrationBuilder.AddColumn<int[]>(
                name: "PositionsAvailable",
                table: "Jobs",
                type: "integer[]",
                nullable: true);
        }
    }
}
