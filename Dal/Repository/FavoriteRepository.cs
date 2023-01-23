using Thing.Context;
using Thing.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Thing.Repository
{
    public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task<bool> IsFavoriteExistsAsync(Favorite favorite) => await Entities.AnyAsync(f => f.UserId == favorite.UserId && f.ProductId == favorite.ProductId).ConfigureAwait(false);
    }
}