﻿using Domain.Models;
using System.Linq.Expressions;

namespace Dal.Repository.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IReadOnlyCollection<Comment>> FindWithAnswersImageUserAsync(Expression<Func<Comment, bool>> conditon);

        Task<Comment?> DeleteAndReturn(int id);
    }
}