﻿// <auto-generated />
using CafeCarki.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CafeCarki.Migrations
{
    [DbContext(typeof(WebDbContext))]
    partial class WebDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CafeCarki.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressDoor");

                    b.Property<string>("AddressFromLocation");

                    b.Property<string>("AddressName");

                    b.Property<string>("AddressStreet");

                    b.Property<string>("GeoError");

                    b.Property<string>("GeoLocationAccuracy");

                    b.Property<string>("GeoLocationLatitude");

                    b.Property<string>("GeoLocationLongitude");

                    b.Property<int?>("RestaurantId");

                    b.Property<bool>("UseGeo");

                    b.Property<string>("UserId");

                    b.Property<bool>("Visible");

                    b.HasKey("AddressId");

                    b.HasIndex("RestaurantId")
                        .IsUnique()
                        .HasFilter("[RestaurantId] IS NOT NULL");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("CafeCarki.Models.Basket", b =>
                {
                    b.Property<int>("BasketId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId");

                    b.HasKey("BasketId");

                    b.ToTable("Basket");
                });

            modelBuilder.Entity("CafeCarki.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("RestaurantId");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("CommentId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CafeCarki.Models.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ParentId");

                    b.Property<decimal>("Price");

                    b.Property<string>("Title");

                    b.Property<bool>("isContainer");

                    b.HasKey("MenuId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("CafeCarki.Models.MenuCategory", b =>
                {
                    b.Property<int>("MenuCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MenuCategoryId1");

                    b.Property<int?>("MenuRestaurantId");

                    b.Property<string>("Name");

                    b.Property<bool>("isChild");

                    b.HasKey("MenuCategoryId");

                    b.HasIndex("MenuCategoryId1");

                    b.HasIndex("MenuRestaurantId");

                    b.ToTable("MenuCategory");
                });

            modelBuilder.Entity("CafeCarki.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BasketId");

                    b.Property<bool>("Broadcast");

                    b.Property<int?>("MenuCategoryId");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("MenuItemId");

                    b.HasIndex("BasketId");

                    b.HasIndex("MenuCategoryId");

                    b.ToTable("MenuItem");
                });

            modelBuilder.Entity("CafeCarki.Models.MenuRestaurant", b =>
                {
                    b.Property<int>("MenuRestaurantId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("UserId");

                    b.HasKey("MenuRestaurantId");

                    b.ToTable("MenuRestaurant");
                });

            modelBuilder.Entity("CafeCarki.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AdminOkey");

                    b.Property<bool>("Broadcast");

                    b.Property<string>("ClosedTime");

                    b.Property<int?>("MenuRestaurantId");

                    b.Property<string>("Name");

                    b.Property<string>("OpenTime");

                    b.Property<int?>("RestaurantId1");

                    b.Property<string>("UserId");

                    b.Property<bool>("isSube");

                    b.HasKey("RestaurantId");

                    b.HasIndex("MenuRestaurantId");

                    b.HasIndex("RestaurantId1");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("CafeCarki.Models.Address", b =>
                {
                    b.HasOne("CafeCarki.Models.Restaurant", "Restaurant")
                        .WithOne("Address")
                        .HasForeignKey("CafeCarki.Models.Address", "RestaurantId");
                });

            modelBuilder.Entity("CafeCarki.Models.Comment", b =>
                {
                    b.HasOne("CafeCarki.Models.Restaurant", "Restaurant")
                        .WithMany("Comment")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("CafeCarki.Models.MenuCategory", b =>
                {
                    b.HasOne("CafeCarki.Models.MenuCategory")
                        .WithMany("MenuCategories")
                        .HasForeignKey("MenuCategoryId1");

                    b.HasOne("CafeCarki.Models.MenuRestaurant", "MenuRestaurant")
                        .WithMany("MenuCategories")
                        .HasForeignKey("MenuRestaurantId");
                });

            modelBuilder.Entity("CafeCarki.Models.MenuItem", b =>
                {
                    b.HasOne("CafeCarki.Models.Basket")
                        .WithMany("MenuItems")
                        .HasForeignKey("BasketId");

                    b.HasOne("CafeCarki.Models.MenuCategory", "MenuCategory")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuCategoryId");
                });

            modelBuilder.Entity("CafeCarki.Models.Restaurant", b =>
                {
                    b.HasOne("CafeCarki.Models.MenuRestaurant", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuRestaurantId");

                    b.HasOne("CafeCarki.Models.Restaurant")
                        .WithMany("Sube")
                        .HasForeignKey("RestaurantId1");
                });
#pragma warning restore 612, 618
        }
    }
}
