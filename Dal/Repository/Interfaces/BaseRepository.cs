using Thing.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Thing.Repository.Interfaces
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        protected ThingDbContext _db;

        protected BaseRepository(ThingDbContext context)
        {
            _db = context;
            _entities = _db.Set<TEntity>();
        }

        protected DbSet<TEntity> Entities => _entities;

        public virtual async Task CreateAsync(TEntity entity)
        {
            await Entities.AddAsync(entity).ConfigureAwait(false);
            await _db.SaveChangesAsync();
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> conditon)
            => await Entities.Where(conditon).ToListAsync().ConfigureAwait(false);

        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
            => await Entities.ToListAsync().ConfigureAwait(false);
    }
}