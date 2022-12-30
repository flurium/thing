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
        public DbSet<Product> Products { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<CategoryProperty> CategoryProperties { get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }

        // Models Creating
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Order
            builder.Entity<Order>().HasKey(o => new { o.UserId, o.ProductId });
            builder.Entity<Order>().HasOne(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductId);
            builder.Entity<Order>().HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);

            // Seller
            builder.Entity<Seller>().HasKey(s => s.Id);
            /// super important IsRequired for EF Core
            builder.Entity<Seller>().HasOne(s => s.User).WithOne(u => u.Seller).IsRequired(false).HasForeignKey<Seller>(s => s.Id);

            ////// Product
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().HasOne(p => p.Seller).WithMany(s => s.Products).HasForeignKey(p => p.SellerId);
            builder.Entity<Product>().Property(p => p.Description).HasColumnType("nchar(300)");
            builder.Entity<Product>().Property(p => p.Price).HasColumnType("money");

            // ProductImage
            builder.Entity<ProductImage>().HasKey(pi => pi.Id);
            builder.Entity<ProductImage>().HasOne(pi => pi.Product).WithMany(p => p.Images).HasForeignKey(pi => pi.ProductId);

            // ProductCategory
            builder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductId, pc.CategoryId });
            builder.Entity<ProductCategory>().HasOne(pc => pc.Category).WithMany(c => c.ProductCategories).HasForeignKey(x => x.CategoryId);
            builder.Entity<ProductCategory>().HasOne(pc => pc.Product).WithMany(p => p.ProductCategories).HasForeignKey(x => x.ProductId);

            // Category
            builder.Entity<Category>().HasKey(c => c.Id);

            // Comment
            builder.Entity<Comment>().HasKey(c => c.Id);
            builder.Entity<Comment>().HasOne(c => c.Product).WithMany(p => p.Comments).HasForeignKey(c => c.ProductId);
            builder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments).IsRequired(false).HasForeignKey(c => c.UserId);
            builder.Entity<Comment>().Property(c => c.Content).HasColumnType("nchar(300)");
            builder.Entity<Comment>().Property(c => c.Date).HasColumnType("date").HasDefaultValue(DateTime.Now);

            // Answer
            builder.Entity<Answer>().HasKey(a => a.Id);
            builder.Entity<Answer>().HasOne(a => a.User).WithMany(u => u.Answers).IsRequired(false).HasForeignKey(a => a.UserId);
            builder.Entity<Answer>().HasOne(a => a.Comment).WithMany(c => c.Answers).HasForeignKey(a => a.CommentId);

            // Favorite
            builder.Entity<Favorite>().HasKey(f => new { f.UserId, f.ProductId });
            builder.Entity<Favorite>().HasOne(f => f.User).WithMany(u => u.Favorites).HasForeignKey(f => f.UserId);
            builder.Entity<Favorite>().HasOne(f => f.Product).WithMany(p => p.Favorites).HasForeignKey(f => f.ProductId);

            // CategoryProperty
            builder.Entity<CategoryProperty>().HasKey(cp => new { cp.PropertyId, cp.CategoryId });
            builder.Entity<CategoryProperty>().HasOne(cp => cp.Category).WithMany(c => c.CategoryProperties).HasForeignKey(cp => cp.CategoryId);
            builder.Entity<CategoryProperty>().HasOne(cp => cp.Property).WithMany(p => p.CategoryProperties).HasForeignKey(cp => cp.PropertyId);

            // Property
            builder.Entity<Property>().HasKey(p => p.Id);

            // PropertyValue
            builder.Entity<PropertyValue>().HasKey(pv => pv.Id);
            builder.Entity<PropertyValue>().HasOne(pv => pv.Property).WithMany(p => p.PropertyValues).HasForeignKey(pv => pv.PropertyId);
            builder.Entity<PropertyValue>().HasOne(pv => pv.Product).WithMany(p => p.PropertyValues).HasForeignKey(pv => pv.ProductId);
        }
    }
}