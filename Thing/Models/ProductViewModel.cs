using Domain.Models;

namespace Dal.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public string SellerId { get; set; }
        public Seller Seller { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IFormFileCollection Url { get; set; }
    }
}