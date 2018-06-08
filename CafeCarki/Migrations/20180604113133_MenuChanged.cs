using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CafeCarki.Migrations
{
    public partial class MenuChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurant_MenuRestaurantId",
                table: "Restaurant");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_MenuRestaurantId",
                table: "Restaurant",
                column: "MenuRestaurantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Restaurant_MenuRestaurantId",
                table: "Restaurant");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_MenuRestaurantId",
                table: "Restaurant",
                column: "MenuRestaurantId",
                unique: true,
                filter: "[MenuRestaurantId] IS NOT NULL");
        }
    }
}
