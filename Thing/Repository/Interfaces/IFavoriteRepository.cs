using Dal.Models;

namespace Dal.Repository.Interfaces
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task Delete(string UserId, int ProductId);

        Task<bool> IsFavoriteExistsAsync(Favorite favorite);
    }
}