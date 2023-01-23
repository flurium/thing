using Domain.Models;

namespace Thing.Repository.Interfaces
{
    public interface ICustomPropertyRepository : IRepository<CustomProperty>
    {
        Task Delete(int Id);
    }
}