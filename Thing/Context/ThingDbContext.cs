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
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Order> Orders { get; set; }

        // Models Creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            OnOrderCreating(builder);
            OnCommentCreating(builder);
            
            modelBuilder.Entity<Category>().HasMany<ProductCategory>(x => x.Category);
        }
        
        private void OnCommentCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasOne<Product>(x => x.Product).WithMany(x => x.Comments).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Comment>().HasOne<User>(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Comment>().Property(p => p.Summary).HasColumnType("nchar(300)");
            modelBuilder.Entity<Comment>().Property(p => p.CommentDate).HasColumnType("date").HasDefaultValue(DateTime.Now); ;
        }

        protected void OnOrderCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(o => new { o.UserId, o.ProductId });
            builder.Entity<Order>().HasOne<Product>(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductId);
            builder.Entity<Order>().HasOne<User>(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);
        }

    }
}
