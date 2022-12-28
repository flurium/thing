namespace Thing.Models
{
    public class Product
    {
        public ICollection<Order> Orders{ get; set; }
        public ICollection<Comment> Comments { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
