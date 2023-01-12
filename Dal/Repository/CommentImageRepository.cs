using Microsoft.EntityFrameworkCore;
using Dal.Context;
using Domain.Models;
using Dal.Repository.Interfaces;

namespace Dal.Repository
{
    public class CommentImageRepository : BaseRepository<CommentImage>, ICommentImageRepository
    {
        public CommentImageRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int Id)
        {
            var commentImage = await Entities.FirstOrDefaultAsync(o => o.Id == Id).ConfigureAwait(false);
            if (commentImage != null) Entities.Remove(commentImage);
            _db.SaveChanges();
        }
    }
}