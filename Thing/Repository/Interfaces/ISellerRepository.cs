using Dal.Models;

namespace Dal.Repository.Interfaces
{
    public interface ISellerRepository : IRepository<Seller>
    {
        public Task Edit(string id, bool isBanned);

        public Task Edit(Seller seller);

        public Task Delete(string id);
    }
}