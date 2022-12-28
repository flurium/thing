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


        private void Comment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasOne<Product>(x => x.Product).WithMany(x => x.Comments).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Comment>().HasOne<User>(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);


            modelBuilder.Entity<Comment>().Property(p => p.Summary).HasColumnType("nchar(300)");
            modelBuilder.Entity<Comment>().Property(p => p.CommentDate).HasColumnType("date").HasDefaultValue(DateTime.Now); ;
        }
        public DbSet<Comment> Comments { get; set; }
    }
}
