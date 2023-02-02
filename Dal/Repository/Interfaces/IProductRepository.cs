using Domain.Models;
using System.Linq.Expressions;

namespace Dal.Repository.Interfaces
{
  public interface IProductRepository : IRepository<Product>
  {
    Task<Product> CreateAndReturnAsync(Product entity);

    Task Delete(int id);

    Task<Product?> DeleteAndReturn(int id);

    Task<IReadOnlyCollection<Product>> FindByConditionsAsync(IEnumerable<Expression<Func<Product, bool>>> conditons, bool includeSeller = true);

    Task<Product?> FindWithImagesProps(int id);

    Task<IReadOnlyCollection<Product>> FindWithImages(Expression<Func<Product, bool>> condition);
  }
}