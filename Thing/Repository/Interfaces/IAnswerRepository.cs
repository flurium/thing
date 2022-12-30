using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task Delete(int Id);
        Task Edit(int Id,string Content);
    }
}
