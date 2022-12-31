namespace Thing.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }

        public ICollection<CategoryProperty> CategoryProperties { get; set; }
        public ICollection<PropertyValue> PropertyValues { get; set; }
    }
}