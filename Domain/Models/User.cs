using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public Seller? Seller { get; set; }

        public IReadOnlyCollection<Comment> Comments { get; set; }

        public IReadOnlyCollection<Favorite> Favorites { get; set; }

        public IReadOnlyCollection<Answer> Answers { get; set; }

        public IReadOnlyCollection<Order> Orders { get; set; }
    }
}