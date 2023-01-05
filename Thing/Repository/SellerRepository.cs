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
    }
}