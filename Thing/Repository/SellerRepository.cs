using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class SellerRepository : BaseRepository<Seller>, ISellerRepository
    {
        public SellerRepository(ThingDbContext context) : base(context)
        {
        }

        public Task Edit(string id, bool isBanned)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string id)
        {
            var seller = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (seller != null) Entities.Remove(seller);
        }

        public async Task Edit(Seller seller)
        {
            Entities.Update(seller);
            await _db.SaveChangesAsync();
        }
    }
}