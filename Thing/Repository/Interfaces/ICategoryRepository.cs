using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task Delete(int Id);
    }
}
