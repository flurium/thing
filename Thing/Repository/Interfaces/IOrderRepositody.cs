using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IOrderRepositody: IRepository<Order>
    {
        Task DeleteAsync(string userId, int productId);
    }
}
