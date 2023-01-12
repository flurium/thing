using Domain.Models;

namespace Dal.Repository.Interfaces
{
    public interface IRequiredPropertyRepository : IRepository<RequiredProperty>
    {
        Task Delete(int id);
    }
}