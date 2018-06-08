using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CafeCarki.Migrations
{
    public partial class MenuFinish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MenuRestaurant",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MenuItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuCategoryId1",
                table: "MenuCategory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuRestaurantId",
                table: "MenuCategory",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isChild",
                table: "MenuCategory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategory_MenuCategoryId1",
                table: "MenuCategory",
                column: "MenuCategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategory_MenuRestaurantId",
                table: "MenuCategory",
                column: "MenuRestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategory_MenuCategory_MenuCategoryId1",
                table: "MenuCategory",
                column: "MenuCategoryId1",
                principalTable: "MenuCategory",
                principalColumn: "MenuCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategory_MenuRestaurant_MenuRestaurantId",
                table: "MenuCategory",
                column: "MenuRestaurantId",
                principalTable: "MenuRestaurant",
                principalColumn: "MenuRestaurantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategory_MenuCategory_MenuCategoryId1",
                table: "MenuCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategory_MenuRestaurant_MenuRestaurantId",
                table: "MenuCategory");

            migrationBuilder.DropIndex(
                name: "IX_MenuCategory_MenuCategoryId1",
                table: "MenuCategory");

            migrationBuilder.DropIndex(
                name: "IX_MenuCategory_MenuRestaurantId",
                table: "MenuCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MenuRestaurant");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "MenuCategoryId1",
                table: "MenuCategory");

            migrationBuilder.DropColumn(
                name: "MenuRestaurantId",
                table: "MenuCategory");

            migrationBuilder.DropColumn(
                name: "isChild",
                table: "MenuCategory");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "MenuItem",
                nullable: true,
                oldClrType: typeof(decimal));
        }
    }
}
