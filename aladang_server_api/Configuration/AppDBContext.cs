using System;
using aladang_server_api.Models;
using aladang_server_api.Models.AppAuthorize;
using Microsoft.EntityFrameworkCore;

namespace aladang_server_api.Configuration
{
	public class AppDBContext : DbContext
    {
        public DbSet<AppUserAuthorize>? appUserAuthorizes { get; set; }
        public DbSet<Customer>? customers { get; set; }
        public DbSet<Shop>? shops { get; set; }        
        public DbSet<DeliveryType>? deliveryTypes { get; set; }
        public DbSet<Banner>? banners { get; set; }
        public DbSet<Currency>? currencies { get; set; }
        public DbSet<Exchange>? exchanges { get; set; }
        public DbSet<Locations>? locations { get; set; }
        public DbSet<Order>? orders { get; set; }
        public DbSet<OrderDetail>? orderDetails { get; set; }
        public DbSet<Product>? products { get; set; }
        public DbSet<Report>? reports { get; set; }
        public DbSet<ShopPayment>? shopPayments { get; set; }

        public DbSet<PaymentMethod>? paymentMethods { get; set; }
        public DbSet<Privacy>? privacies { get; set; }
        public DbSet<ProductImage>? productImages { get; set; }
        public DbSet<QRCode>? qRCodes { get; set; }
        public DbSet<ReportType>? reportTypes { get; set; }
        public DbSet<SetUpFee>? setUpFees { get; set; }
        public DbSet<ProductImageDetail>? ProductImageDetails { get; set; }
        public DbSet<OrderTracking> OrderTrackings { get; set; }
        public DbSet<DeliveryTrackingType> DeliveryTrackingTypes { get; set; }
        public object OrderShops { get; internal set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductImageDetail>().HasKey(p => new { p.Id });
            // modelBuilder.Entity<Product>()
            //     .HasMany(e => e.ProductImageDetails)
            //     .WithOne(e => e.Product)
            //     .HasForeignKey(e => e.ProductId)
            //     .IsRequired();
        }

        
    }
}

