using Domain.Models;

namespace Thing.Repository.Interfaces
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task<bool> IsFavoriteExistsAsync(Favorite favorite);
    }
}