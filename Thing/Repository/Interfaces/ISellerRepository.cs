using Microsoft.EntityFrameworkCore;
using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface ISellerRepository : IRepository<Seller>
    {
        public Task Edit(string id, bool isBanned);
    }
}