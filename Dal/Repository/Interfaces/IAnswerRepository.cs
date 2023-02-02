using Domain.Models;

namespace Dal.Repository.Interfaces
{
  public interface IAnswerRepository : IRepository<Answer>
  {
    Task Delete(int Id);
  }
}