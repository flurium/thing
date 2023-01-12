using Dal.Models;

namespace Dal.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task Delete(int Id);
    }
}