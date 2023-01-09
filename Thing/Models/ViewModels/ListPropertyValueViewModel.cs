namespace Thing.Models.ViewModels
{
    public class ListPropertyValueViewModel
    {
        public int Id { get; set; }
        public List<string> Values { get; set; }
        public List<int> PropertyId { get; set; }
    }
}