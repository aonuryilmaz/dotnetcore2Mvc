using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CafeCarki.Migrations
{
    public partial class Changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_Address_AddressId",
                table: "Restaurant");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Restaurant",
                newName: "RestaurantId1");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurant_AddressId",
                table: "Restaurant",
                newName: "IX_Restaurant_RestaurantId1");

            migrationBuilder.AddColumn<bool>(
                name: "isSube",
                table: "Restaurant",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Address",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_RestaurantId",
                table: "Address",
                column: "RestaurantId",
                unique: true,
                filter: "[RestaurantId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Restaurant_RestaurantId",
                table: "Address",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_Restaurant_RestaurantId1",
                table: "Restaurant",
                column: "RestaurantId1",
                principalTable: "Restaurant",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Restaurant_RestaurantId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_Restaurant_RestaurantId1",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Address_RestaurantId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "isSube",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "RestaurantId1",
                table: "Restaurant",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurant_RestaurantId1",
                table: "Restaurant",
                newName: "IX_Restaurant_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_Address_AddressId",
                table: "Restaurant",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
