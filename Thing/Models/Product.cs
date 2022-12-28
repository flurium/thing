namespace Thing.Models
{
    public class Product
    {
        public ICollection<Order> Orders{ get; set;}
    }
}
