using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thing.Models;

namespace Thing.Context
{
    public class ThingDbContext : IdentityDbContext<User>
    {
        public ThingDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        // DbSets
        public DbSet<Order> Orders { get; set; }

        // Models Creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            OnOrderCreating(builder);
        }

        protected void OnOrderCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(o => new { o.UserId, o.ProductId });
            builder.Entity<Order>().HasOne<Product>(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductId);
            builder.Entity<Order>().HasOne<User>(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);
        }

    }
}
