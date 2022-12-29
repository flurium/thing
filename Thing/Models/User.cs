using Microsoft.AspNetCore.Identity;

namespace Thing.Models
{
    public class User : IdentityUser
    {
        public List<Comment> Comments { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<Favorite> Favorites { get; set; }

        /// <summary> Products which user sells </summary>
        public ICollection<Product> SellProducts { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}