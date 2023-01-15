using Domain.Models;

namespace Dal.Repository.Interfaces
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task<bool> IsFavoriteExistsAsync(Favorite favorite);
    }
}