﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using web.Data;

namespace web.Migrations
{
    [DbContext(typeof(KioskContext))]
    [Migration("20180405012039_KioskDb")]
    partial class KioskDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("web.Data.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<int>("AppliedAwardPoints")
                        .HasColumnType("int(11)");

                    b.Property<decimal>("AppliedDiscount")
                        .HasColumnType("decimal(10,0)");

                    b.Property<string>("BuyerId")
                        .HasMaxLength(50);

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(10,0)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId")
                        .HasName("BuyerId");

                    b.HasIndex("SellerId")
                        .HasName("SellerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("web.Data.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50);

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Quantity")
                        .HasColumnType("int(11)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .HasName("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("web.Data.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Image")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("Quantity")
                        .HasColumnType("int(11)");

                    b.Property<string>("SellerId")
                        .HasMaxLength(50);

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(10,0)");

                    b.HasKey("Id");

                    b.HasIndex("SellerId")
                        .HasName("SellerId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("web.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<int?>("AwardPoints")
                        .HasColumnType("int(11)");

                    b.Property<string>("CreditCard")
                        .HasMaxLength(500);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("char(2)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("web.Data.Order", b =>
                {
                    b.HasOne("web.Data.User", "Buyer")
                        .WithMany("OrderBuyer")
                        .HasForeignKey("BuyerId")
                        .HasConstraintName("Order_ibfk_2");

                    b.HasOne("web.Data.User", "Seller")
                        .WithMany("OrderSeller")
                        .HasForeignKey("SellerId")
                        .HasConstraintName("Order_ibfk_1");
                });

            modelBuilder.Entity("web.Data.OrderItem", b =>
                {
                    b.HasOne("web.Data.Product", "IdNavigation")
                        .WithOne("OrderItem")
                        .HasForeignKey("web.Data.OrderItem", "Id")
                        .HasConstraintName("OrderItem_ibfk_2");

                    b.HasOne("web.Data.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("OrderItem_ibfk_1");
                });

            modelBuilder.Entity("web.Data.Product", b =>
                {
                    b.HasOne("web.Data.User", "Seller")
                        .WithMany("Product")
                        .HasForeignKey("SellerId")
                        .HasConstraintName("Product_ibfk_1");
                });
#pragma warning restore 612, 618
        }
    }
}
