using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GLFManager.App.Migrations
{
    public partial class AddedIsCompletedToJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsJobComplete",
                table: "Jobs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "JobsId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobsId",
                table: "Employees",
                column: "JobsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Jobs_JobsId",
                table: "Employees",
                column: "JobsId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Jobs_JobsId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_JobsId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsJobComplete",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobsId",
                table: "Employees");
        }
    }
}
