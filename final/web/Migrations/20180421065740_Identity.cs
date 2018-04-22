using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace web.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<string>(maxLength: 50, nullable: false),
                    CategoryName = table.Column<string>(maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    CreditCardId = table.Column<string>(maxLength: 50, nullable: false),
                    CreditCardCVV = table.Column<int>(type: "int(11)", nullable: true),
                    CreditCardExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreditCardFirstName = table.Column<string>(maxLength: 30, nullable: true),
                    CreditCardLastName = table.Column<string>(maxLength: 30, nullable: true),
                    CreditCardNumber = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.CreditCardId);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<string>(maxLength: 50, nullable: false),
                    UserTypeName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ApplicationUserAwardPoints = table.Column<int>(type: "int(11)", nullable: true),
                    ApplicationUserCreditCardId = table.Column<string>(maxLength: 50, nullable: true),
                    ApplicationUserEmail = table.Column<string>(maxLength: 250, nullable: false),
                    ApplicationUserFirstName = table.Column<string>(maxLength: 30, nullable: false),
                    ApplicationUserId = table.Column<string>(maxLength: 50, nullable: true),
                    ApplicationUserLastName = table.Column<string>(maxLength: 30, nullable: false),
                    ApplicationUserPassword = table.Column<string>(maxLength: 100, nullable: false),
                    ApplicationUserPhoneNumber = table.Column<string>(maxLength: 45, nullable: true),
                    ApplicationUserTypeId = table.Column<string>(maxLength: 50, nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "ApplicationUser_CreditCard",
                        column: x => x.ApplicationUserCreditCardId,
                        principalTable: "CreditCard",
                        principalColumn: "CreditCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "ApplicationUserTypeId_UserType",
                        column: x => x.ApplicationUserTypeId,
                        principalTable: "UserType",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<string>(maxLength: 50, nullable: false),
                    OrderAppliedAwardPoints = table.Column<int>(type: "int(11)", nullable: true),
                    OrderAppliedDiscount = table.Column<double>(nullable: true),
                    OrderBillingAddress1 = table.Column<string>(maxLength: 300, nullable: false),
                    OrderBillingAddress2 = table.Column<string>(maxLength: 300, nullable: true),
                    OrderBillingCity = table.Column<string>(maxLength: 45, nullable: false),
                    OrderBillingFirstName = table.Column<string>(maxLength: 45, nullable: false),
                    OrderBillingLastName = table.Column<string>(maxLength: 45, nullable: false),
                    OrderBillingState = table.Column<string>(maxLength: 45, nullable: false),
                    OrderBillingZipCode = table.Column<string>(maxLength: 45, nullable: false),
                    OrderCreditCardId = table.Column<string>(maxLength: 50, nullable: true),
                    OrderCustomerId = table.Column<string>(maxLength: 50, nullable: true),
                    OrderDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderShippingAddress1 = table.Column<string>(maxLength: 300, nullable: false),
                    OrderShippingAddress2 = table.Column<string>(maxLength: 300, nullable: true),
                    OrderShippingCity = table.Column<string>(maxLength: 45, nullable: false),
                    OrderShippingFirstName = table.Column<string>(maxLength: 45, nullable: false),
                    OrderShippingLastName = table.Column<string>(maxLength: 45, nullable: false),
                    OrderShippingState = table.Column<string>(maxLength: 45, nullable: false),
                    OrderShippingZipCode = table.Column<int>(type: "int(11)", nullable: false),
                    OrderTotal = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "Order_CreditCard",
                        column: x => x.OrderCreditCardId,
                        principalTable: "CreditCard",
                        principalColumn: "CreditCardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Orderr_ApplicationUser",
                        column: x => x.OrderCustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<string>(maxLength: 50, nullable: false),
                    ProductCategoryId = table.Column<string>(maxLength: 50, nullable: false),
                    ProductDescription = table.Column<string>(maxLength: 300, nullable: true),
                    ProductExpirationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProductImage = table.Column<string>(maxLength: 100, nullable: true),
                    ProductName = table.Column<string>(maxLength: 50, nullable: false),
                    ProductQuantity = table.Column<int>(type: "int(11)", nullable: false),
                    ProductUnitPrice = table.Column<double>(nullable: false),
                    ProductVendorId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "Product_Category",
                        column: x => x.ProductCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Product_ApplicationUser",
                        column: x => x.ProductVendorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<string>(maxLength: 50, nullable: false),
                    OrderItemOrderId = table.Column<string>(maxLength: 50, nullable: false),
                    OrderItemProductId = table.Column<string>(maxLength: 45, nullable: false),
                    OrderItemQuantity = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "OrderItem_Order",
                        column: x => x.OrderItemOrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "OrderItem_Product",
                        column: x => x.OrderItemProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItem",
                columns: table => new
                {
                    ShoppingCartItemId = table.Column<string>(maxLength: 50, nullable: false),
                    ShoppingCartId = table.Column<string>(maxLength: 50, nullable: false),
                    ShoppingCartItemAmount = table.Column<int>(type: "int(11)", nullable: false),
                    ShoppingCartItemProductId = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItem", x => x.ShoppingCartItemId);
                    table.ForeignKey(
                        name: "ShoppingCartItem_Product",
                        column: x => x.ShoppingCartItemProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "ApplicationUser_CreditCard_idx",
                table: "AspNetUsers",
                column: "ApplicationUserCreditCardId");

            migrationBuilder.CreateIndex(
                name: "ApplicationUserType_UserType_idx",
                table: "AspNetUsers",
                column: "ApplicationUserTypeId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CategoryName_UNIQUE",
                table: "Category",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Order_CreditCard_idx",
                table: "Order",
                column: "OrderCreditCardId");

            migrationBuilder.CreateIndex(
                name: "OrderBuyer_BusinessUser_idx",
                table: "Order",
                column: "OrderCustomerId");

            migrationBuilder.CreateIndex(
                name: "OrderItem_Order",
                table: "OrderItem",
                column: "OrderItemOrderId");

            migrationBuilder.CreateIndex(
                name: "OrderItem_Product_idx",
                table: "OrderItem",
                column: "OrderItemProductId");

            migrationBuilder.CreateIndex(
                name: "Product_Category_idx",
                table: "Product",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "Product_ApplicationUser_idx",
                table: "Product",
                column: "ProductVendorId");

            migrationBuilder.CreateIndex(
                name: "ShoppingCartItem_Product",
                table: "ShoppingCartItem",
                column: "ShoppingCartItemProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ShoppingCartItem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
