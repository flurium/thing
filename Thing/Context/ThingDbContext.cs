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

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }


        private void Productfunc()
        {
           /*
            modelBuilder.Entity<ProductCategory>().HasKey(x => new { x.ProductId, x.CategoryId });
            modelBuilder.Entity<Product>().HasMany<ProductCategory>(x => x.ProductCategories);
            modelBuilder.Entity<Product>().Property(p => p.Description).HasColumnType("nchar(300)");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("money");
           */
        }
    }
}
