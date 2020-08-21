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

        public DbSet<Image> Images { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ProductTag> ProductTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductTag>().HasKey(c => new { c.ProductId, c.TagId });
            base.OnModelCreating(builder);
        }
    }
}
