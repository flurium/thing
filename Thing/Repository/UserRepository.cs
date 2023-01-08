using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserById(string Id) => Entities.FirstOrDefault(u=>u.Id == Id);
    }
}
