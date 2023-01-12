using Dal.Models;

namespace Dal.Repository.Interfaces
{
    public interface ICustomPropertyRepository : IRepository<CustomProperty>
    {
        Task Delete(int Id);
    }
}