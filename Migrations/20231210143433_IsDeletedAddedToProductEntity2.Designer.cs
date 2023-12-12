﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreApp.API.Data;

#nullable disable

namespace StoreApp.API.Migrations
{
    [DbContext(typeof(StoreAppDbContext))]
    [Migration("20231210143433_IsDeletedAddedToProductEntity2")]
    partial class IsDeletedAddedToProductEntity2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StoreApp.API.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryTotalCount")
                        .HasColumnType("int");

                    b.Property<double>("CategoryTotalPrice")
                        .HasColumnType("double");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryTotalCount = 0,
                            CategoryTotalPrice = 0.0,
                            Description = "This is electronics category",
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            CategoryTotalCount = 0,
                            CategoryTotalPrice = 0.0,
                            Description = "This is books category",
                            Name = "Books"
                        },
                        new
                        {
                            Id = 3,
                            CategoryTotalCount = 0,
                            CategoryTotalPrice = 0.0,
                            Description = "This is food and drinks category",
                            Name = "Foods And Drinks"
                        },
                        new
                        {
                            Id = 4,
                            CategoryTotalCount = 0,
                            CategoryTotalPrice = 0.0,
                            Description = "This is clothes category",
                            Name = "Clothes"
                        });
                });

            modelBuilder.Entity("StoreApp.API.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "This has RTX-4090...",
                            IsDeleted = false,
                            Name = "Gaming Laptop",
                            Price = 2000.25,
                            Quantity = 10
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "This is ps5...",
                            IsDeleted = false,
                            Name = "PS5",
                            Price = 1000.0,
                            Quantity = 15
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Description = "This has lotr...",
                            IsDeleted = false,
                            Name = "Lord Of The Rings - Two Tower",
                            Price = 24.0,
                            Quantity = 20
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 3,
                            Description = "This is water",
                            IsDeleted = false,
                            Name = "Water - 5lt",
                            Price = 5.0,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 4,
                            Description = "This is hoodie for men",
                            IsDeleted = false,
                            Name = "Black Men Hoodie",
                            Price = 64.0,
                            Quantity = 40
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 4,
                            Description = "This is parfume for men",
                            IsDeleted = false,
                            Name = "Men Parfume - BrandName",
                            Price = 39.990000000000002,
                            Quantity = 60
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 1,
                            Description = "This is latest iphone",
                            IsDeleted = false,
                            Name = "IPhone 15 Pro Max - 1TB",
                            Price = 1299.99,
                            Quantity = 10
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 1,
                            Description = "This is high-performance gaming pc",
                            IsDeleted = false,
                            Name = "Gaming Pc - 4090 | i9 | 16gbram",
                            Price = 2900.0,
                            Quantity = 15
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 4,
                            Description = "This is sport men shoes",
                            IsDeleted = false,
                            Name = "Men Nike Air Max",
                            Price = 150.0,
                            Quantity = 100
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 1,
                            Description = "This is gaming keyboard",
                            IsDeleted = false,
                            Name = "Logitech Gaming Keyboard",
                            Price = 75.0,
                            Quantity = 25
                        });
                });

            modelBuilder.Entity("StoreApp.API.Data.Entities.Product", b =>
                {
                    b.HasOne("StoreApp.API.Data.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("StoreApp.API.Data.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}