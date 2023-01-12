namespace Domain.Models
{
    /// <summary>
    /// Properties connected to categories
    /// Required in product of category
    /// </summary>
    public class RequiredProperty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<RequiredPropertyValue> PropertyValues { get; set; }
    }
}