using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Thing.Models
{
    public class Seller
    {
        public string Id { get; set; }

        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}