﻿using Thing.Context;
using Thing.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Thing.Repository
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