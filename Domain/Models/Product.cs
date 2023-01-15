namespace Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public string SellerId { get; set; }
        public Seller Seller { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IReadOnlyCollection<Order> Orders { get; set; }
        public IReadOnlyCollection<Comment> Comments { get; set; }
        public IReadOnlyCollection<Favorite> Favorites { get; set; }
        public IReadOnlyCollection<ProductImage> Images { get; set; }
        public IReadOnlyCollection<RequiredPropertyValue> RequiredPropertyValues { get; set; }
        public IReadOnlyCollection<CustomProperty> CustomProperties { get; set; }
    }
}