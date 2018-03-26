using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentHubApplication.API.Migrations
{
    public partial class _4AddingNotesColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ApplicationQualifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ApplicationCourseCampuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ApplicationQualifications");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ApplicationCourseCampuses");
        }
    }
}
