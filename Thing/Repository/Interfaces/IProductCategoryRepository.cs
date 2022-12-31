using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        Task Delete(int productId, int categoryId);
    }
}