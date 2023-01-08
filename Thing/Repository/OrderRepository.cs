using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepositody
    {
        public OrderRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task DeleteAsync(string userId, int productId)
        {
            var order = await Entities.FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == productId).ConfigureAwait(false);
            if (order != null) Entities.Remove(order);
        }

        public async Task<bool> IsOrderExistsAsync(Order order) => await Entities.AnyAsync(o => o.UserId == order.UserId && o.ProductId == order.ProductId).ConfigureAwait(false);
    }
}