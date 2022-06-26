using CatalogWebApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogWebApp.DAL
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>()
                .HasData(Seed.Data);
        }

        public DbSet<ProductModel> Catalogs { get; set; } = null!;

        public DbSet<ProductImageModel> CatalogImages { get; set; } = null!;
    }
}
