using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public virtual async Task<IReadOnlyCollection<Order>> FindByConditionWithPropertiesAsync(Expression<Func<Order, bool>> conditon)
            => await Entities.Include(o => o.UserId).Where(conditon).ToListAsync().ConfigureAwait(false);
    }
}