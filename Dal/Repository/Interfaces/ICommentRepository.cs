using Domain.Models;

namespace Dal.Repository.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task Delete(int Id);
    }
}