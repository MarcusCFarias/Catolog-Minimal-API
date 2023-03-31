using CatalogApi.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace WebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //category
            mb.Entity<Category>().HasKey(c => c.Id);
            mb.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            mb.Entity<Category>().Property(c => c.Description).HasMaxLength(150);

            //product
            mb.Entity<Product>().HasKey(c => c.Id);
            mb.Entity<Product>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            mb.Entity<Product>().Property(c => c.Description).HasMaxLength(150);
            mb.Entity<Product>().Property(c => c.ImgUrl).HasMaxLength(100);
            mb.Entity<Product>().Property(c => c.Price).HasPrecision(14, 2);

            //relationship
            mb.Entity<Product>()
                .HasOne<Category>(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}