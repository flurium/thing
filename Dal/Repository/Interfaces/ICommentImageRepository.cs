using Domain.Models;

namespace Thing.Repository.Interfaces
{
    public interface ICommentImageRepository : IRepository<CommentImage>
    {
        Task Delete(int Id);
    }
}