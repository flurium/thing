using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Order> Orders { get; set; }


        // Models Creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>().HasOne<Category>(x => x.Category)
                .WithMany(x => x.ProductCategories).HasForeignKey(x => x.CategoryId);
            builder.Entity<ProductCategory>().HasOne<Product>(x => x.Product)
                .WithMany(x => x.ProductCategories).HasForeignKey(x => x.ProductId);
        
            builder.Entity<Comment>().HasOne<Product>(x => x.Product).WithMany(x => x.Comments).HasForeignKey(x => x.ProductId);
            builder.Entity<Comment>().HasOne<User>(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);
            builder.Entity<Comment>().Property(p => p.Summary).HasColumnType("nchar(300)");
            builder.Entity<Comment>().Property(p => p.CommentDate).HasColumnType("date").HasDefaultValue(DateTime.Now); ;
        
            builder.Entity<Order>().HasKey(o => new { o.UserId, o.ProductId });
            builder.Entity<Order>().HasOne<Product>(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductId);
            builder.Entity<Order>().HasOne<User>(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);

            builder.Entity<ProductCategory>().HasKey(x => new { x.ProductId, x.CategoryId });
            builder.Entity<Product>().HasMany<ProductCategory>(x => x.ProductCategories);
            builder.Entity<Product>().Property(p => p.Description).HasColumnType("nchar(300)");
            builder.Entity<Product>().Property(p => p.Price).HasColumnType("money");
        }

    }
}
