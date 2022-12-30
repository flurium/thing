using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IPropertyRepository:IRepository<Property>
    {
        Task Delete(int id);
    }
}
