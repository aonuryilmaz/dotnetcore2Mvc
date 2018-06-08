using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CafeCarki.Migrations
{
    public partial class Fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminOkey",
                table: "Restaurant",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Broadcaste",
                table: "Restaurant",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ClosedTime",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenTime",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressFromLocation",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoError",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoLocationAccuracy",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoLocationLatitude",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoLocationLongitude",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseGeo",
                table: "Address",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminOkey",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "Broadcaste",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "ClosedTime",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "AddressFromLocation",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "GeoError",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "GeoLocationAccuracy",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "GeoLocationLatitude",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "GeoLocationLongitude",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "UseGeo",
                table: "Address");
        }
    }
}
