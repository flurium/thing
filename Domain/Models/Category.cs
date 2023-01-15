namespace Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IReadOnlyCollection<Product> Products { get; set; }
        public IReadOnlyCollection<RequiredProperty> RequiredProperties { get; set; }
    }
}