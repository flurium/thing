namespace Thing.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public string SellerId { get; set; }
        public User Seller { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<PropertyValue> PropertyValues { get; set; }
    }
}