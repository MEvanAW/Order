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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
