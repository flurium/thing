using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dal.Context;
using Domain.Models;
using Dal.Repository.Interfaces;

namespace Dal.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int Id)
        {
            var comment = await Entities.FirstOrDefaultAsync(o => o.Id == Id).ConfigureAwait(false);
            if (comment != null) Entities.Remove(comment);
            await _db.SaveChangesAsync();
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