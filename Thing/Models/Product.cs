namespace Thing.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Status { get;  set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
