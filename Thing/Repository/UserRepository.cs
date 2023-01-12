using Dal.Context;
using Dal.Models;
using Dal.Repository.Interfaces;

namespace Dal.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserById(string Id) => Entities.FirstOrDefault(u => u.Id == Id);
    }
}