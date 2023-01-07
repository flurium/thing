using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task Delete(int id);

        Task Edit(Product product);
        Task <Product> GetByIdAsync(int id);
    }
}