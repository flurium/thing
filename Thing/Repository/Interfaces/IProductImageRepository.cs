using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task Delete(int id);
    }
}