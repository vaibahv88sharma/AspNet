using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentHubApplication.API.Migrations
{
    public partial class _6IsPrimaryLocationMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ApplicationQualifications",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ApplicationCourseCampuses",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ApplicationQualifications",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ApplicationCourseCampuses",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
