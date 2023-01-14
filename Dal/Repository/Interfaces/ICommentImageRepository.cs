using Domain.Models;
using System.Linq.Expressions;

namespace Dal.Repository.Interfaces
{
    public interface ICommentImageRepository : IRepository<CommentImage>
    {
        Task Delete(int Id);
		Task<IReadOnlyCollection<Comment>> FindWithAnswersImageUserAsync(Expression<Func<Comment, bool>> conditon);
		Task<Comment?> DeleteAndReturn(int id);

	}
}