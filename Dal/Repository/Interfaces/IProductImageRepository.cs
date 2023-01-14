using Domain.Models;
using System.Linq.Expressions;

namespace Dal.Repository.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task Delete(int id);

        Task<IReadOnlyCollection<ProductImage>> FindByConditionAsync(Expression<Func<ProductImage, bool>> conditon);

		Task<ProductImage> GetImageByProductIdAsync(int id);
    }
}