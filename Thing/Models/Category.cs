namespace Thing.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<RequiredProperty> RequiredProperties { get; set; }
    }
}