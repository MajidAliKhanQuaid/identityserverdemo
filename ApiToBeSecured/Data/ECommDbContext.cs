using ApiToBeSecured.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToBeSecured.Data
{
    public class ECommDbContext : DbContext
    {
        public ECommDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BuyingList> BuyingList { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<ProductShipments> ProductShipments { get; set; }

        public DbSet<SoldList> SoldList { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProductTag> ProductTags { get; set; }

        public DbSet<ShipmentProductQuantity> ShipmentProductQuantity { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Shipment>().HasKey(c => new { c.UserId, c.ProductId });
            builder.Entity<ProductTag>().HasKey(c => new { c.ProductId, c.TagId });
            base.OnModelCreating(builder);
        }
    }
}
