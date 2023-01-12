using Dal.Models;

namespace Dal.Repository.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        Task Delete(int id);

        Task<ProductImage> GetImageByProductIdAsync(int id);
    }
}