using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thing.Models;

namespace Thing.Context
{
    public class ThingDbContext : IdentityDbContext<User>
    {
        public ThingDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
