using Dal.Models;

namespace Dal.Repository.Interfaces
{
    public interface ICommentImageRepository : IRepository<CommentImage>
    {
        Task Delete(int Id);
    }
}