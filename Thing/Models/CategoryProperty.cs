namespace Thing.Models
{
    public class CategoryProperty
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}