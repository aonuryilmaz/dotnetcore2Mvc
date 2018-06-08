using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CafeCarki.Data.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressFromLocation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GeoError",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GeoLocationAccuracy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GeoLocationLatitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GeoLocationLongitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ilce",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Mahalle",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UseGeo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressFromLocation",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoError",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoLocationAccuracy",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoLocationLatitude",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoLocationLongitude",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ilce",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mahalle",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UseGeo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
