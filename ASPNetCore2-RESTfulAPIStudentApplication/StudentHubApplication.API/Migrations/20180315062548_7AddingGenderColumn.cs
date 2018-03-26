using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StudentHubApplication.API.Migrations
{
    public partial class _7AddingGenderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Applications",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Applications");
        }
    }
}
