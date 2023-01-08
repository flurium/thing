using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserById(string Id);
    }
}