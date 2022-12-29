using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IPtoductRepository:IRepository<Product>
    {
        Task Delete(int id);

        Task Edit(Product product);
    }
}
