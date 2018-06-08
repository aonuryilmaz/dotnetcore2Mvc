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
    [Migration("20180501143434_ChangedRestaurant")]
    partial class ChangedRestaurant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("CafeCarki.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AdminOkey");

                    b.Property<bool>("Broadcast");

                    b.Property<string>("ClosedTime");

                    b.Property<string>("Name");

                    b.Property<string>("OpenTime");

                    b.Property<int?>("RestaurantId1");

                    b.Property<string>("UserId");

                    b.Property<bool>("isSube");

                    b.HasKey("RestaurantId");

                    b.HasIndex("RestaurantId1");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("CafeCarki.Models.Address", b =>
                {
                    b.HasOne("CafeCarki.Models.Restaurant", "Restaurant")
                        .WithOne("Address")
                        .HasForeignKey("CafeCarki.Models.Address", "RestaurantId");
                });

            modelBuilder.Entity("CafeCarki.Models.Restaurant", b =>
                {
                    b.HasOne("CafeCarki.Models.Restaurant")
                        .WithMany("Sube")
                        .HasForeignKey("RestaurantId1");
                });
#pragma warning restore 612, 618
        }
    }
}