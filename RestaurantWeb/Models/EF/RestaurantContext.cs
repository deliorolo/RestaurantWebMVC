using System.Data.Entity;

namespace RestaurantWeb.Models.EF
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RestaurantContext, RestaurantWeb.Migrations.Configuration>());
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<SoldProductAccomplished> SoldProductsAccomplished { get; set; }
    }
}