using System.Linq.Expressions;

namespace Thing.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<IReadOnlyCollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> conditon);
        Task CreateAsync(TEntity entity);
    }
}
