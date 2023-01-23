using Thing.Context;
using Thing.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Thing.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task DeleteAsync(string userId, int productId)
        {
            var order = await Entities.FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == productId).ConfigureAwait(false);
            if (order != null) Entities.Remove(order);
            await _db.SaveChangesAsync();
        }

        public virtual async Task<IReadOnlyCollection<Order>> FindIncludeProductsAsync(Expression<Func<Order, bool>> conditon)
            => await Entities.Include(o => o.Product).Where(conditon).ToListAsync().ConfigureAwait(false);

        public async Task Edit(Order order)
        {
            Entities.Update(order);
            await _db.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(string userId, int productId)
        {
            return await Entities.FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == productId);
        }

        public async Task Increase(string userId, int productId)
        {
            var order = await Entities.FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == productId).ConfigureAwait(false);
            if (order != null)
            {
                order.Count++;
                await Edit(order);
            }
        }
    }
}