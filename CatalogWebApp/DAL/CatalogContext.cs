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

            modelBuilder.Entity<CatalogModel>()
                .HasData(Seed.Data);
        }

        public DbSet<CatalogModel> Catalogs { get; set; } = null!;

        public DbSet<CatalogImageModel> CatalogImages { get; set; } = null!;
    }
}
