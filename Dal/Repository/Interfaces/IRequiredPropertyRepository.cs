using Domain.Models;

namespace Thing.Repository.Interfaces
{
    public interface IRequiredPropertyRepository : IRepository<RequiredProperty>
    {
        Task<RequiredProperty?> CreateAndReturnAsync(RequiredProperty entity);

        Task Update(int id, string name);

        Task Delete(int id);
    }
}