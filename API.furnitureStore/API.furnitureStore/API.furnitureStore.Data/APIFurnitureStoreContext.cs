using API.furnitureStore.shared;
using Microsoft.EntityFrameworkCore;

namespace API.furnitureStore.Data
{
    internal class APIFurnitureStoreContext : DbContext
    {
        public APIFurnitureStoreContext(DbContextOptions options):base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<productCategory> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }
    }
}
