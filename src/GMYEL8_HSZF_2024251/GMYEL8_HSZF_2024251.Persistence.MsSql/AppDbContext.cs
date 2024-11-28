using GMYEL8_HSZF_2024251.Model.Entities;

using Microsoft.EntityFrameworkCore;

namespace GMYEL8_HSZF_2024251.Persistence.MsSql
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaxiCar> TaxiCars { get; set; }
        public DbSet<Service> Services { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            //Database.Migrate();
        }
    }
}
