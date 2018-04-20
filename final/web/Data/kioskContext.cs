using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using web.Data.Entities;

namespace web.Data
{
    public partial class KioskContext : DbContext
    {
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CreditCard> CreditCard { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        public KioskContext(DbContextOptions<Data.KioskContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => e.ApplicationUserCreditCardId)
                    .HasName("ApplicationUser_CreditCard_idx");

                entity.HasIndex(e => e.ApplicationUserTypeId)
                    .HasName("ApplicationUserType_UserType_idx");

                entity.Property(e => e.ApplicationUserId).HasMaxLength(50);

                entity.Property(e => e.ApplicationUserAwardPoints).HasColumnType("int(11)");

                entity.Property(e => e.ApplicationUserCreditCardId).HasMaxLength(50);

                entity.Property(e => e.ApplicationUserEmail)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ApplicationUserFirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ApplicationUserLastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ApplicationUserPassword)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ApplicationUserPhoneNumber).HasMaxLength(45);

                entity.Property(e => e.ApplicationUserTypeId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ApplicationUserCreditCard)
                    .WithMany(p => p.ApplicationUser)
                    .HasForeignKey(d => d.ApplicationUserCreditCardId)
                    .HasConstraintName("ApplicationUser_CreditCard");

                entity.HasOne(d => d.ApplicationUserType)
                    .WithMany(p => p.ApplicationUser)
                    .HasForeignKey(d => d.ApplicationUserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ApplicationUserTypeId_UserType");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.CategoryName)
                    .HasName("CategoryName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasMaxLength(50);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<CreditCard>(entity =>
            {
                entity.Property(e => e.CreditCardId).HasMaxLength(50);

                entity.Property(e => e.CreditCardCvv)
                    .HasColumnName("CreditCardCVV")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreditCardExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.CreditCardFirstName).HasMaxLength(30);

                entity.Property(e => e.CreditCardLastName).HasMaxLength(30);

                entity.Property(e => e.CreditCardNumber).HasMaxLength(100);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderCreditCardId)
                    .HasName("Order_CreditCard_idx");

                entity.HasIndex(e => e.OrderCustomerId)
                    .HasName("OrderBuyer_BusinessUser_idx");

                entity.Property(e => e.OrderId).HasMaxLength(50);

                entity.Property(e => e.OrderAppliedAwardPoints).HasColumnType("int(11)");

                entity.Property(e => e.OrderBillingAddress1)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.OrderBillingAddress2).HasMaxLength(300);

                entity.Property(e => e.OrderBillingCity)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderBillingFirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderBillingLastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderBillingState)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderBillingZipCode)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderCreditCardId).HasMaxLength(50);

                entity.Property(e => e.OrderCustomerId).HasMaxLength(50);

                entity.Property(e => e.OrderDateTime).HasColumnType("datetime");

                entity.Property(e => e.OrderShippingAddress1)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.OrderShippingAddress2).HasMaxLength(300);

                entity.Property(e => e.OrderShippingCity)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderShippingFirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderShippingLastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderShippingState)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderShippingZipCode).HasColumnType("int(11)");

                entity.HasOne(d => d.OrderCreditCard)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderCreditCardId)
                    .HasConstraintName("Order_CreditCard");

                entity.HasOne(d => d.OrderCustomer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderCustomerId)
                    .HasConstraintName("Orderr_ApplicationUser");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasIndex(e => e.OrderItemOrderId)
                    .HasName("OrderItem_Order");

                entity.HasIndex(e => e.OrderItemProductId)
                    .HasName("OrderItem_Product_idx");

                entity.Property(e => e.OrderItemId).HasMaxLength(50);

                entity.Property(e => e.OrderItemOrderId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderItemProductId)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.OrderItemQuantity).HasColumnType("int(11)");

                entity.HasOne(d => d.OrderItemOrder)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderItemOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderItem_Order");

                entity.HasOne(d => d.OrderItemProduct)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderItemProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderItem_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ProductCategoryId)
                    .HasName("Product_Category_idx");

                entity.HasIndex(e => e.ProductVendorId)
                    .HasName("Product_ApplicationUser_idx");

                entity.Property(e => e.ProductId).HasMaxLength(50);

                entity.Property(e => e.ProductCategoryId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductDescription).HasMaxLength(300);

                entity.Property(e => e.ProductExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.ProductImage).HasMaxLength(100);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductQuantity).HasColumnType("int(11)");

                entity.Property(e => e.ProductVendorId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_Category");

                entity.HasOne(d => d.ProductVendor)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductVendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_ApplicationUser");
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasIndex(e => e.ShoppingCartItemProductId)
                    .HasName("ShoppingCartItem_Product");

                entity.Property(e => e.ShoppingCartItemId).HasMaxLength(50);

                entity.Property(e => e.ShoppingCartId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShoppingCartItemAmount).HasColumnType("int(11)");

                entity.Property(e => e.ShoppingCartItemProductId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ShoppingCartItemProduct)
                    .WithMany(p => p.ShoppingCartItem)
                    .HasForeignKey(d => d.ShoppingCartItemProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ShoppingCartItem_Product");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.UserTypeId).HasMaxLength(50);

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }
    }
}
