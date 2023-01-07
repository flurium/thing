using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IRequiredPropertyRepository : IRepository<RequiredProperty>
    {
        Task Delete(int id);
    }
}