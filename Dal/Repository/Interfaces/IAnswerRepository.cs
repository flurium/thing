using Domain.Models;

namespace Thing.Repository.Interfaces
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task Delete(int Id);
    }
}