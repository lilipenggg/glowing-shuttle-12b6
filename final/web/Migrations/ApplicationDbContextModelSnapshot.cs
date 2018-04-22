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
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("web.Data.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("ApplicationUserAwardPoints")
                        .HasColumnType("int(11)");

                    b.Property<string>("ApplicationUserCreditCardId")
                        .HasMaxLength(50);

                    b.Property<string>("ApplicationUserEmail")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("ApplicationUserFirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("ApplicationUserLastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("ApplicationUserPhoneNumber")
                        .HasMaxLength(45);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserCreditCardId")
                        .HasName("ApplicationUser_CreditCard_idx");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("web.Data.Entities.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.HasKey("CategoryId");

                    b.HasIndex("CategoryName")
                        .IsUnique()
                        .HasName("CategoryName_UNIQUE");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("web.Data.Entities.CreditCard", b =>
                {
                    b.Property<string>("CreditCardId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<int?>("CreditCardCvv")
                        .HasColumnName("CreditCardCVV")
                        .HasColumnType("int(11)");

                    b.Property<DateTime?>("CreditCardExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CreditCardFirstName")
                        .HasMaxLength(30);

                    b.Property<string>("CreditCardLastName")
                        .HasMaxLength(30);

                    b.Property<string>("CreditCardNumber")
                        .HasMaxLength(100);

                    b.HasKey("CreditCardId");

                    b.ToTable("CreditCard");
                });

            modelBuilder.Entity("web.Data.Entities.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<int?>("OrderAppliedAwardPoints")
                        .HasColumnType("int(11)");

                    b.Property<double?>("OrderAppliedDiscount");

                    b.Property<string>("OrderBillingAddress1")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("OrderBillingAddress2")
                        .HasMaxLength(300);

                    b.Property<string>("OrderBillingCity")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("OrderBillingFirstName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("OrderBillingLastName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("OrderBillingState")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("OrderBillingZipCode")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("OrderCreditCardId")
                        .HasMaxLength(50);

                    b.Property<string>("OrderCustomerId")
                        .HasMaxLength(50);

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("OrderShippingAddress1")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("OrderShippingAddress2")
                        .HasMaxLength(300);

                    b.Property<string>("OrderShippingCity")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("OrderShippingFirstName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("OrderShippingLastName")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<string>("OrderShippingState")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<int>("OrderShippingZipCode")
                        .HasColumnType("int(11)");

                    b.Property<double?>("OrderTotal");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderCreditCardId")
                        .HasName("Order_CreditCard_idx");

                    b.HasIndex("OrderCustomerId")
                        .HasName("OrderBuyer_BusinessUser_idx");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("web.Data.Entities.OrderItem", b =>
                {
                    b.Property<string>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("OrderItemOrderId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OrderItemProductId")
                        .IsRequired()
                        .HasMaxLength(45);

                    b.Property<int>("OrderItemQuantity")
                        .HasColumnType("int(11)");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderItemOrderId")
                        .HasName("OrderItem_Order");

                    b.HasIndex("OrderItemProductId")
                        .HasName("OrderItem_Product_idx");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("web.Data.Entities.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("ProductCategoryId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ProductDescription")
                        .HasMaxLength(300);

                    b.Property<DateTime?>("ProductExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("ProductImage")
                        .HasMaxLength(100);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int(11)");

                    b.Property<double>("ProductUnitPrice");

                    b.Property<string>("ProductVendorId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryId")
                        .HasName("Product_Category_idx");

                    b.HasIndex("ProductVendorId")
                        .HasName("Product_ApplicationUser_idx");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("web.Data.Entities.ShoppingCartItem", b =>
                {
                    b.Property<string>("ShoppingCartItemId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("ShoppingCartId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ShoppingCartItemAmount")
                        .HasColumnType("int(11)");

                    b.Property<string>("ShoppingCartItemProductId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ShoppingCartItemId");

                    b.HasIndex("ShoppingCartItemProductId")
                        .HasName("ShoppingCartItem_Product");

                    b.ToTable("ShoppingCartItem");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("web.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("web.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("web.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("web.Data.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("web.Data.Entities.ApplicationUser", b =>
                {
                    b.HasOne("web.Data.Entities.CreditCard", "ApplicationUserCreditCard")
                        .WithMany("ApplicationUser")
                        .HasForeignKey("ApplicationUserCreditCardId")
                        .HasConstraintName("ApplicationUser_CreditCard");
                });

            modelBuilder.Entity("web.Data.Entities.Order", b =>
                {
                    b.HasOne("web.Data.Entities.CreditCard", "OrderCreditCard")
                        .WithMany("Order")
                        .HasForeignKey("OrderCreditCardId")
                        .HasConstraintName("Order_CreditCard");

                    b.HasOne("web.Data.Entities.ApplicationUser", "OrderCustomer")
                        .WithMany("Order")
                        .HasForeignKey("OrderCustomerId")
                        .HasConstraintName("Orderr_ApplicationUser");
                });

            modelBuilder.Entity("web.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("web.Data.Entities.Order", "OrderItemOrder")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderItemOrderId")
                        .HasConstraintName("OrderItem_Order");

                    b.HasOne("web.Data.Entities.Product", "OrderItemProduct")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderItemProductId")
                        .HasConstraintName("OrderItem_Product");
                });

            modelBuilder.Entity("web.Data.Entities.Product", b =>
                {
                    b.HasOne("web.Data.Entities.Category", "ProductCategory")
                        .WithMany("Product")
                        .HasForeignKey("ProductCategoryId")
                        .HasConstraintName("Product_Category");

                    b.HasOne("web.Data.Entities.ApplicationUser", "ProductVendor")
                        .WithMany("Product")
                        .HasForeignKey("ProductVendorId")
                        .HasConstraintName("Product_ApplicationUser");
                });

            modelBuilder.Entity("web.Data.Entities.ShoppingCartItem", b =>
                {
                    b.HasOne("web.Data.Entities.Product", "ShoppingCartItemProduct")
                        .WithMany("ShoppingCartItem")
                        .HasForeignKey("ShoppingCartItemProductId")
                        .HasConstraintName("ShoppingCartItem_Product");
                });
#pragma warning restore 612, 618
        }
    }
}
