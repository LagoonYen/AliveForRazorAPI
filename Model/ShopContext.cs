using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AliveStoreTemplate.Model
{
    public partial class ShopContext : DbContext
    {
        public ShopContext()
        {
        }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MemberInfo> MemberInfos { get; set; }
        public virtual DbSet<MemberShopcar> MemberShopcars { get; set; }
        public virtual DbSet<OrderAddress> OrderAddresses { get; set; }
        public virtual DbSet<OrderList> OrderLists { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<ProductList> ProductLists { get; set; }
        public virtual DbSet<ProductShopcar> ProductShopcars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost;Database=PTCGShop;UID=sa;Password=cgp3716;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberInfo>(entity =>
            {
                entity.ToTable("MemberInfo");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.NickName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ProfilePhotoUrl).HasColumnName("ProfilePhotoURL");

                entity.Property(e => e.RegisterTime).HasColumnType("datetime");

                entity.Property(e => e.Sex).HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<MemberShopcar>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("member_shopcar");

                entity.Property(e => e.CardName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ImgUrl).HasColumnName("ImgURL");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Subcategory)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Uid).HasColumnName("uid");
            });

            modelBuilder.Entity<OrderAddress>(entity =>
            {
                entity.ToTable("orderAddress");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.Town).HasMaxLength(50);

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updateTime");
            });

            modelBuilder.Entity<OrderList>(entity =>
            {
                entity.ToTable("order_list");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("addressId");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.IsPay).HasColumnName("is_pay");

                entity.Property(e => e.IsReceipt).HasColumnName("is_receipt");

                entity.Property(e => e.IsShip).HasColumnName("is_ship");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("order_number");

                entity.Property(e => e.PayPrice).HasColumnName("pay_price");

                entity.Property(e => e.PayTime).HasColumnName("pay_time");

                entity.Property(e => e.ReceiptTime).HasColumnName("receipt_time");

                entity.Property(e => e.Recipient)
                    .HasMaxLength(50)
                    .HasColumnName("recipient");

                entity.Property(e => e.RecipientAddress)
                    .HasMaxLength(50)
                    .HasColumnName("recipientAddress");

                entity.Property(e => e.RecipientCity)
                    .HasMaxLength(50)
                    .HasColumnName("recipientCity");

                entity.Property(e => e.RecipientPhone)
                    .HasMaxLength(50)
                    .HasColumnName("recipientPhone");

                entity.Property(e => e.RecipientTown)
                    .HasMaxLength(50)
                    .HasColumnName("recipientTown");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("remark");

                entity.Property(e => e.ShipNumber)
                    .HasMaxLength(50)
                    .HasColumnName("ship_number");

                entity.Property(e => e.ShipTime).HasColumnName("ship_time");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("order_product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductNum).HasColumnName("product_num");

                entity.Property(e => e.ProductPrice).HasColumnName("product_price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");
            });

            modelBuilder.Entity<ProductList>(entity =>
            {
                entity.ToTable("ProductList");

                entity.Property(e => e.CardName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ImgUrl).HasColumnName("ImgURL");

                entity.Property(e => e.RealseTime).HasColumnType("datetime");

                entity.Property(e => e.Subcategory)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductShopcar>(entity =>
            {
                entity.ToTable("product_shopcar");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
