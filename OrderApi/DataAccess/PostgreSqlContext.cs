using Microsoft.EntityFrameworkCore;
using OrderApi.Models;

namespace OrderApi.DataAccess
{
    public class PostgreSqlContext: DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options): base(options)
        {
        }
        public DbSet<Order> orders { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<User> users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Item>().ToTable("items");
            modelBuilder.Entity<User>().ToTable("users");
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
