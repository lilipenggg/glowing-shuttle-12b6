using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace web.Data
{
    public partial class kioskContext : DbContext
    {
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=138.68.9.183;Port=3306;Database=kiosk;user=localuser;Password=newPassw0rd!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.BuyerId)
                    .HasName("BuyerId");

                entity.HasIndex(e => e.SellerId)
                    .HasName("SellerId");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AppliedAwardPoints).HasColumnType("int(11)");

                entity.Property(e => e.AppliedDiscount).HasColumnType("decimal(10,0)");

                entity.Property(e => e.BuyerId).HasMaxLength(50);

                entity.Property(e => e.OrderDateTime).HasColumnType("datetime");

                entity.Property(e => e.SellerId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(10,0)");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.OrderBuyer)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("Order_ibfk_2");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.OrderSeller)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Order_ibfk_1");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasIndex(e => e.OrderId)
                    .HasName("OrderId");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.OrderItem)
                    .HasForeignKey<OrderItem>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderItem_ibfk_2");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderItem_ibfk_1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.SellerId)
                    .HasName("SellerId");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Quantity).HasColumnType("int(11)");

                entity.Property(e => e.SellerId).HasMaxLength(50);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,0)");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("Product_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.AwardPoints).HasColumnType("int(11)");

                entity.Property(e => e.CreditCard).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("char(2)");
            });
        }
    }
}
