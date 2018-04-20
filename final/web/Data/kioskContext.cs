using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using web.Data.Entities;

namespace web.Data
{
    public partial class KioskContext : DbContext
    {
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<BusinessUser> BusinessUser { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CreditCard> CreditCard { get; set; }
        public virtual DbSet<GuestUser> GuestUser { get; set; }
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
                entity.HasIndex(e => e.ApplicationUserTypeId)
                    .HasName("ApplicationUserType_UserType_idx");

                entity.Property(e => e.ApplicationUserId).HasMaxLength(50);

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

                entity.Property(e => e.ApplicationUserTypeId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ApplicationUserType)
                    .WithMany(p => p.ApplicationUser)
                    .HasForeignKey(d => d.ApplicationUserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ApplicationUserTypeId_UserType");
            });

            modelBuilder.Entity<BusinessUser>(entity =>
            {
                entity.HasIndex(e => e.BusinessUserCreditCardId)
                    .HasName("BusinessUser_CreditCard_idx");

                entity.Property(e => e.BusinessUserId).HasMaxLength(50);

                entity.Property(e => e.BusinessUserAwardPoints).HasColumnType("int(11)");

                entity.Property(e => e.BusinessUserBillingAddress)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BusinessUserBillingAddress2).HasMaxLength(200);

                entity.Property(e => e.BusinessUserBillingCity)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.BusinessUserBillingState)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.BusinessUserBillingZipCode).HasColumnType("int(11)");

                entity.Property(e => e.BusinessUserCreditCardId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BusinessUserShippingAddress)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.BusinessUserShippingAddress2).HasMaxLength(200);

                entity.Property(e => e.BusinessUserShippingCity)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.BusinessUserShippingState)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.BusinessUserShippingZipCode).HasColumnType("int(11)");

                entity.HasOne(d => d.BusinessUserCreditCard)
                    .WithMany(p => p.BusinessUser)
                    .HasForeignKey(d => d.BusinessUserCreditCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BusinessUser_CreditCard");

                entity.HasOne(d => d.BusinessUserNavigation)
                    .WithOne(p => p.BusinessUser)
                    .HasForeignKey<BusinessUser>(d => d.BusinessUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BusinessUser_ApplicationUser");
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
                    .HasColumnType("int(3)");

                entity.Property(e => e.CreditCardExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.CreditCardFirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.CreditCardLastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.CreditCardNumber)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<GuestUser>(entity =>
            {
                entity.Property(e => e.GuestUserId).HasMaxLength(50);

                entity.Property(e => e.GuestUserBillingAddress)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.GuestUserBillingAddress2)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GuestUserBillingCity)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GuestUserBillingZipCode).HasColumnType("int(11)");

                entity.Property(e => e.GuestUserBillingZipState)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GuestUserFirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GuestUserLastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GuestUserShippingAddress)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.GuestUserShippingAddress2)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GuestUserShippingCity)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GuestUserShippingState)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.GuestUserShippingZipCode).HasColumnType("int(11)");

                entity.HasOne(d => d.GuestUserNavigation)
                    .WithOne(p => p.GuestUser)
                    .HasForeignKey<GuestUser>(d => d.GuestUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("GuestUser_CreditCard");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderBuyerId)
                    .HasName("OrderBuyer_BusinessUser_idx");

                entity.HasIndex(e => e.OrderGuestBuyerId)
                    .HasName("OrderGuestBuyer_GuestUser_idx");

                entity.Property(e => e.OrderId).HasMaxLength(50);

                entity.Property(e => e.OrderAppliedAwardPoints).HasColumnType("int(11)");

                entity.Property(e => e.OrderBuyerId).HasMaxLength(50);

                entity.Property(e => e.OrderDateTime).HasColumnType("datetime");

                entity.Property(e => e.OrderGuestBuyerId).HasMaxLength(50);

                entity.HasOne(d => d.OrderBuyer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderBuyerId)
                    .HasConstraintName("OrderBuyer_BusinessUser");

                entity.HasOne(d => d.OrderGuestBuyer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderGuestBuyerId)
                    .HasConstraintName("OrderGuestBuyer_GuestUser");
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

                entity.HasIndex(e => e.ProductSellerId)
                    .HasName("Product_User_idx");

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

                entity.Property(e => e.ProductSellerId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_Category");

                entity.HasOne(d => d.ProductSeller)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductSellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Product_BusinessUser");
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
