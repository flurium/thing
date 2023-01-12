using Dal.Models;

namespace Dal.Repository.Interfaces
{
    public interface IPropertyValueRepository : IRepository<RequiredPropertyValue>
    {
        Task Delete(int Id);
    }
}