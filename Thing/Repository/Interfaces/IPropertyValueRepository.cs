using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IPropertyValueRepository : IRepository<PropertyValue>
    {
        Task Delete(int Id);
    }
}