using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dal.Context;
using Dal.Models;
using Dal.Repository.Interfaces;

namespace Dal.Repository
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

        public async Task<bool> IsOrderExistsAsync(Order order) => await Entities.AnyAsync(o => o.UserId == order.UserId && o.ProductId == order.ProductId).ConfigureAwait(false);
    }
}