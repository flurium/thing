using Thing.Models;

namespace Thing.Repository.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task Delete(int Id);
    }
}