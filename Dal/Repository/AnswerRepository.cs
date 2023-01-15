using Dal.Context;
using Dal.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repository
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task Delete(int Id)
        {
            var answer = await Entities.FirstOrDefaultAsync(o => o.Id == Id).ConfigureAwait(false);
            if (answer != null) Entities.Remove(answer);
            await _db.SaveChangesAsync();
        }
    }
}