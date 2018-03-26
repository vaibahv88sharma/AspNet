using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentHubApplication.API.Migrations
{
    public partial class _3AddingAdditionalCOlumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "ApplicationQualifications",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryQualification",
                table: "ApplicationQualifications",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryLocation",
                table: "ApplicationCourseCampuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "ApplicationQualifications");

            migrationBuilder.DropColumn(
                name: "IsPrimaryQualification",
                table: "ApplicationQualifications");

            migrationBuilder.DropColumn(
                name: "IsPrimaryLocation",
                table: "ApplicationCourseCampuses");
        }
    }
}
