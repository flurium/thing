namespace Thing.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}