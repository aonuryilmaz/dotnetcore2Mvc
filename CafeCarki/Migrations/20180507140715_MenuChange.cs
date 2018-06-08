using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CafeCarki.Migrations
{
    public partial class MenuChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuRestaurantId",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuCategory",
                columns: table => new
                {
                    MenuCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategory", x => x.MenuCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "MenuRestaurant",
                columns: table => new
                {
                    MenuRestaurantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRestaurant", x => x.MenuRestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Broadcast = table.Column<bool>(nullable: false),
                    MenuCategoryId = table.Column<int>(nullable: true),
                    Price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItem_MenuCategory_MenuCategoryId",
                        column: x => x.MenuCategoryId,
                        principalTable: "MenuCategory",
                        principalColumn: "MenuCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_MenuRestaurantId",
                table: "Restaurant",
                column: "MenuRestaurantId",
                unique: true,
                filter: "[MenuRestaurantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_MenuCategoryId",
                table: "MenuItem",
                column: "MenuCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_MenuRestaurant_MenuRestaurantId",
                table: "Restaurant",
                column: "MenuRestaurantId",
                principalTable: "MenuRestaurant",
                principalColumn: "MenuRestaurantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_MenuRestaurant_MenuRestaurantId",
                table: "Restaurant");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "MenuRestaurant");

            migrationBuilder.DropTable(
                name: "MenuCategory");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_MenuRestaurantId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "MenuRestaurantId",
                table: "Restaurant");
        }
    }
}
