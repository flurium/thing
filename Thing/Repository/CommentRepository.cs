
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System.Linq.Expressions;
using System.Net;
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
            var order = await Entities.FirstOrDefaultAsync(o => o.Id == Id).ConfigureAwait(false);
            if (order != null) Entities.Remove(order);
            _db.SaveChanges();
        }

    }
}