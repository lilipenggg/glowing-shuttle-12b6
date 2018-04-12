using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using web.Data.Entities;

namespace web.Data
{
    public partial class KioskContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public virtual DbSet<User> User { get; set; }

        public KioskContext(DbContextOptions<Data.KioskContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderBuyerId)
                    .HasName("BuyerId");

                entity.HasIndex(e => e.OrderSellerId)
                    .HasName("SellerId");

                entity.Property(e => e.OrderId).HasMaxLength(50);

                entity.Property(e => e.OrderAppliedAwardPoints).HasColumnType("int(11)");

                entity.Property(e => e.OrderBuyerId).HasMaxLength(50);

                entity.Property(e => e.OrderDateTime).HasColumnType("datetime");

                entity.Property(e => e.OrderSellerId).HasMaxLength(50);

                entity.HasOne(d => d.OrderBuyer)
                    .WithMany(p => p.OrderOrderBuyer)
                    .HasForeignKey(d => d.OrderBuyerId)
                    .HasConstraintName("Order_ibfk_2");

                entity.HasOne(d => d.OrderSeller)
                    .WithMany(p => p.OrderOrderSeller)
                    .HasForeignKey(d => d.OrderSellerId)
                    .HasConstraintName("Order_ibfk_1");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasIndex(e => e.OrderItemOrderId)
                    .HasName("OrderId");

                entity.Property(e => e.OrderItemId).HasMaxLength(50);

                entity.Property(e => e.OrderItemOrderId).HasMaxLength(50);

                entity.Property(e => e.OrderItemQuantity).HasColumnType("int(11)");

                entity.HasOne(d => d.OrderItemNavigation)
                    .WithOne(p => p.OrderItem)
                    .HasForeignKey<OrderItem>(d => d.OrderItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderItem_ibfk_2");

                entity.HasOne(d => d.OrderItemOrder)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderItemOrderId)
                    .HasConstraintName("OrderItem_ibfk_1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ProductCategoryId)
                    .HasName("Product_Category_idx");

                entity.HasIndex(e => e.ProductSellerId)
                    .HasName("Product_User");

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
                    .HasConstraintName("Product_User");
            });

            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasIndex(e => e.ProductId)
                    .HasName("ProductId");

                entity.Property(e => e.ShoppingCartItemId).HasMaxLength(50);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShoppingCartId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShoppingCartItemAmount).HasColumnType("int(11)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCartItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ShoppingCartItem_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.UserAwardPoints).HasColumnType("int(11)");

                entity.Property(e => e.UserCreditCard).HasMaxLength(500);

                entity.Property(e => e.UserEmail).HasMaxLength(250);

                entity.Property(e => e.UserFirstName).HasMaxLength(30);

                entity.Property(e => e.UserLastName).HasMaxLength(30);

                entity.Property(e => e.UserPassword).HasMaxLength(100);

                entity.Property(e => e.UserType).HasColumnType("char(2)");
            });
        }
    }
}
