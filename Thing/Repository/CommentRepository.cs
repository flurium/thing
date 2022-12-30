using Microsoft.EntityFrameworkCore;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
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
            _db.SaveChanges();
        }
    }
}