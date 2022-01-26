using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GLFManager.App.Migrations
{
    public partial class RevertedJobsEmployeesInJobsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Jobs_JobsId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_JobsId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "JobsId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JobsId",
                table: "Employees",
                type: "uuid",
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
    }
}
