using API.furnitureStore.shared;
using Microsoft.EntityFrameworkCore;

namespace API.furnitureStore.Data
{
    public class APIFurnitureStoreContext : DbContext
    {
        public APIFurnitureStoreContext(DbContextOptions options):base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<productCategory> ProductCategories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.IdProduct, od.Idorder });
        }
    }
}
