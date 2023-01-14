using Domain.Models;

namespace Dal.Repository.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task DeleteAsync(string userId, int productId);
    }
}