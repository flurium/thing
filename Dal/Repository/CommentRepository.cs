using Dal.Context;
using Dal.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dal.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ThingDbContext context) : base(context)
        {
        }

        public virtual async Task<IReadOnlyCollection<Comment>> FindWithAnswersImageUserAsync(Expression<Func<Comment, bool>> conditon)
            => await Entities.Include(c => c.User).Include(c => c.CommentImages).Include(c => c.Answers).ThenInclude(a => a.User).Where(conditon).ToListAsync().ConfigureAwait(false);

        public async Task<Comment?> DeleteAndReturn(int id)
        {
            var comment = await Entities.Include(c => c.Product).FirstOrDefaultAsync(x => x.Id == id);
            if (comment != null) Entities.Remove(comment);
            await _db.SaveChangesAsync();
            return comment;
        }
    }
}