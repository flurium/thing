using Domain.Models;

namespace Dal.Repository.Interfaces
{
    public interface IOrderRepositody : IRepository<Order>
    {
        Task DeleteAsync(string userId, int productId);
    }
}