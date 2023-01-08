using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(string UserId, int ProductId)
        {
            var favorite = await Entities.FirstOrDefaultAsync(o => o.UserId == UserId && o.ProductId == ProductId).ConfigureAwait(false);
            if (favorite != null) Entities.Remove(favorite);
            _db.SaveChanges();
        }
        public async Task<bool> IsFavoriteExistsAsync(Favorite favorite) => await Entities.AnyAsync(f => f.UserId == favorite.UserId && f.ProductId == favorite.ProductId).ConfigureAwait(false);

    }
}