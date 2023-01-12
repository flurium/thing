namespace Domain.Models
{
    public class Seller
    {
        public string Id { get; set; }

        public string About { get; set; }

        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}