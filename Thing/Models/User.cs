using Microsoft.AspNetCore.Identity;

namespace Thing.Models
{
    public class User: IdentityUser
    {
        public List<Comment> Comments { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
