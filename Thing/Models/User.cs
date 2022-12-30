using Microsoft.AspNetCore.Identity;

namespace Thing.Models
{
    public class User : IdentityUser
    {
        public Seller? Seller { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}