namespace Domain.Models
{
    public class RequiredPropertyValue
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int PropertyId { get; set; }
        public RequiredProperty Property { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}