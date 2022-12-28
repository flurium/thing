using Microsoft.AspNetCore.Identity;

namespace Thing.Models
{
    public class User: IdentityUser
    {




        public ICollection<Order> Orders { get; set; }
    }
}
