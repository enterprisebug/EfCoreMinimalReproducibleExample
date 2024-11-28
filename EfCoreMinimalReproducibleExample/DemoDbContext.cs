using EfCoreMinimalReproducibleExample.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMinimalReproducibleExample
{
    public class DemoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=127.0.0.1,5433;Initial Catalog=DemoDbContextTable;User Id=sa;Password=Pass@word;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<MyEntity> MyEntities => Set<MyEntity>();
    }
}