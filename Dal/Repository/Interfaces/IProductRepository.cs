using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dal.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
		Task<Product> CreateAsync(Product entity);

		Task<Product> FirstOrDefault(Expression<Func<Product, bool>> conditon);

		Task Delete(int id);

		Task<Product?> DeleteAndReturn(int id);

		Task<IReadOnlyCollection<Product>> FindByConditionsAsync(IEnumerable<Expression<Func<Product, bool>>> conditons, bool includeSeller = true);

		Task Edit(Product product);

	    Task<Product?> GetByIdAsync(int id);

		Task<Product?> FindWithImagesProps(int id);

		Task<IReadOnlyCollection<Product>> FindWithImages(Expression<Func<Product, bool>> condition);
	}
}