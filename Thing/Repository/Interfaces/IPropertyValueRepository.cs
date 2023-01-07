using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IPropertyValueRepository : IRepository<RequiredPropertyValue>
    {
        Task Delete(int Id);
    }
}