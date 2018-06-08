using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CafeCarki.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Address",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Address");
        }
    }
}
