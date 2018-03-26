using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentHubApplication.API.Migrations
{
    public partial class _5IsPrimaryLocationRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPrimaryQualification",
                table: "ApplicationQualifications",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ApplicationCourseCampuses",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPrimaryLocation",
                table: "ApplicationCourseCampuses",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPrimaryQualification",
                table: "ApplicationQualifications",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "ApplicationCourseCampuses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPrimaryLocation",
                table: "ApplicationCourseCampuses",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
