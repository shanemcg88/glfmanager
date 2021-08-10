using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GLFManager.App.Migrations
{
    public partial class AddingDateOfJobToJobsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfJob",
                table: "Jobs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfJob",
                table: "Jobs");
        }
    }
}
